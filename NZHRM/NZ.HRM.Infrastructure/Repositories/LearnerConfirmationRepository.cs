using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.LearnerAdjustments.Commands;
using NZ.HRM.Application.Model.LearnerAdjustments.DTOs;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Domain.Enums;
using NZ.HRM.Domain.Services;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class LearnerConfirmationRepository : ILearnerConfirmationRepository
{
    private readonly ApplicationDbContext _context;

    public LearnerConfirmationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<LearnerConfirmationBatchResultDto> ForwardAsync(
        ForwardLearnersForConfirmationCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = new LearnerConfirmationBatchResultDto { TotalRequested = command.EmployeeIds.Count };


        var employees = await _context.HrmEmployeeMasters
            .Include(e => e.Employment)!.ThenInclude(emp => emp!.Designation)
            .Include(e => e.Payroll)
            .Where(e => command.EmployeeIds.Contains(e.Id))
            .ToListAsync(cancellationToken);

        var alreadyPending = await _context.HrmLearnerConfirmationRequests
            .Where(r => command.EmployeeIds.Contains(r.EmployeeId)
                        && r.Status == LearnerConfirmationStatus.Forwarded.ToString())
            .Select(r => r.EmployeeId)
            .ToListAsync(cancellationToken);

        foreach (var employeeId in command.EmployeeIds)
        {
            var employee = employees.FirstOrDefault(e => e.Id == employeeId);

            if (employee is null)
            {
                result.Items.Add(Failure(employeeId, $"Employee {employeeId} not found."));
                continue;
            }

            if (alreadyPending.Contains(employeeId))
            {
                result.Items.Add(Failure(employeeId, "A permanency request is already awaiting approval."));
                continue;
            }

            var joiningDate = employee.Employment?.JoiningDate;
            if (joiningDate is null)
            {
                result.Items.Add(Failure(employeeId, "Joining date is not available."));
                continue;
            }

            var currentGrossSalary = employee.Payroll?.GrossSalary;
            if (currentGrossSalary is null || currentGrossSalary <= 0m)
            {
                result.Items.Add(Failure(employeeId, "Current gross salary is not available."));
                continue;
            }

            var standardGrossSalary = await GetStandardWorkerGrossSalaryAsync(employee.Employment?.GradeId, cancellationToken);
            if (standardGrossSalary is null || standardGrossSalary <= 0m)
            {
                result.Items.Add(Failure(employeeId, "Standard worker gross salary is not configured."));
                continue;
            }

            var probationCompletedOn = ProbationAdjustmentPolicy
                .CalculateProbationCompletedOn(joiningDate.Value, command.ProbationPeriodMonths);

            var request = HrmLearnerConfirmationRequest.Forward(
                employeeId,
                joiningDate.Value,
                command.ProbationPeriodMonths,
                probationCompletedOn,
                decimal.Round(currentGrossSalary.Value, 2, MidpointRounding.AwayFromZero),
                decimal.Round(standardGrossSalary.Value, 2, MidpointRounding.AwayFromZero),
                ProbationAdjustmentPolicy.CalculateAdjustmentAmount(standardGrossSalary.Value, currentGrossSalary.Value),
                command.ForwardedBy,
                string.Empty);

            request.CreatedBy = command.ForwardedBy;
            request.UpdatedBy = command.ForwardedBy;

            await _context.HrmLearnerConfirmationRequests.AddAsync(request, cancellationToken);
            await _context.HrmLearnerConfirmationApprovalHistories.AddRangeAsync(
                request.ApprovalHistories,
                cancellationToken);

            result.Items.Add(new LearnerConfirmationResultItemDto
            {
                EmployeeId = employeeId,
                RequestId = request.Id,
                Status = request.Status,
                Succeeded = true
            });
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Finalize(result);
    }

    public async Task<LearnerConfirmationBatchResultDto> ApproveAsync(
        ApproveLearnerConfirmationsCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = new LearnerConfirmationBatchResultDto { TotalRequested = command.Requests.Count };

        var requestIds = command.Requests
            .Select(request => request.RequestId)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        var requests = await _context.HrmLearnerConfirmationRequests
            .Where(r => requestIds.Contains(r.Id)
                        && r.Status == LearnerConfirmationStatus.Forwarded.ToString())
            .ToListAsync(cancellationToken);

        var requestsById = requests.ToDictionary(request => request.Id, StringComparer.OrdinalIgnoreCase);

        var employees = await _context.HrmEmployeeMasters
            .Include(e => e.Employment)
            .Include(e => e.Payroll)
            .Where(e => requests.Select(request => request.EmployeeId).Contains(e.Id))
            .ToListAsync(cancellationToken);

        foreach (var action in command.Requests)
        {
            var requestId = action.RequestId;
            var request = requestsById.GetValueOrDefault(requestId);

            if (request is null)
            {
                result.Items.Add(Failure(requestId, "No permanency request awaiting approval was found."));
                continue;
            }

            if (!action.Approved)
            {
                var history = request.Reject(command.ApprovedBy, action.Remarks);
                request.UpdatedBy = command.ApprovedBy;
                _context.HrmLearnerConfirmationApprovalHistories.Add(history);
                result.Items.Add(Success(request));
                continue;
            }

            var employee = employees.FirstOrDefault(e => e.Id == request.EmployeeId);
            if (employee?.Employment is null || employee.Payroll is null)
            {
                result.Items.Add(Failure(requestId, "Employment or payroll information is not available."));
                continue;
            }

            var approvalHistory = request.Approve(command.ApprovedBy, action.Remarks);
            request.UpdatedBy = command.ApprovedBy;
            _context.HrmLearnerConfirmationApprovalHistories.Add(approvalHistory);

            // Apply permanency to the employee record.
            employee.Employment.ConfirmationDate = request.ProbationCompletedOn;
            employee.Employment.UpdatedBy = command.ApprovedBy;
            employee.Payroll.GrossSalary = request.StandardGrossSalary;
            employee.Payroll.UpdatedBy = command.ApprovedBy;

            result.Items.Add(Success(request));
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Finalize(result);
    }

    public async Task<LearnerConfirmationBatchResultDto> ForwardToMovementCellAsync(List<LearnerConfirmationsCommand> command, CancellationToken cancellationToken)
    {
        var result = new LearnerConfirmationBatchResultDto { TotalRequested = command.Count };

        var requestIds = command
            .Select(request => request.RequestId)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        var requests = await _context.HrmLearnerConfirmationRequests
            .Where(r => requestIds.Contains(r.Id)
                        && r.Status.ToLower() == LearnerConfirmationStatus.Approved.ToString().ToLower())
            .ToListAsync(cancellationToken);

        var requestsById = requests.ToDictionary(request => request.Id, StringComparer.OrdinalIgnoreCase);

        var employees = await _context.HrmEmployeeMasters
            .Include(e => e.Employment)
            .Include(e => e.Payroll)
            .Where(e => requests.Select(request => request.EmployeeId).Contains(e.Id))
            .ToListAsync(cancellationToken);

        foreach (var action in command)
        {
            var requestId = action.RequestId;
            var request = requestsById.GetValueOrDefault(requestId);

            if (request is null)
            {
                result.Items.Add(Failure(requestId, "No permanency request awaiting approval was found."));
                continue;
            }


            var employee = employees.FirstOrDefault(e => e.Id == request.EmployeeId);
            if (employee?.Employment is null || employee.Payroll is null)
            {
                result.Items.Add(Failure(requestId, "Employment or payroll information is not available."));
                continue;
            }

            var approvalHistory = request.ForwardToMovementSection(action.ForwardedBy, action.Remarks);
            request.UpdatedBy = action.ForwardedBy;
            _context.HrmLearnerConfirmationApprovalHistories.Add(approvalHistory);

            // Apply permanency to the employee record.
            employee.Employment.ConfirmationDate = request.ProbationCompletedOn;
            employee.Employment.UpdatedBy = action.ForwardedBy;
            employee.Payroll.GrossSalary = request.StandardGrossSalary;
            employee.Payroll.UpdatedBy = action.ForwardedBy;

            result.Items.Add(Success(request));
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Finalize(result);
    }

    public async Task<LearnerConfirmationBatchResultDto> ForwardToMovementSectionAsync(List<LearnerConfirmationsCommand> commands, CancellationToken cancellationToken)
    {
        var result = new LearnerConfirmationBatchResultDto { TotalRequested = commands.Count };

        var requestIds = commands
            .Select(request => request.RequestId)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        var requests = await _context.HrmLearnerConfirmationRequests
            .Where(r => requestIds.Contains(r.Id)
                        && r.Status.ToLower() == LearnerConfirmationStatus.ForwardedMovementSection.ToString().ToLower())
            .ToListAsync(cancellationToken);

        var requestsById = requests.ToDictionary(request => request.Id, StringComparer.OrdinalIgnoreCase);

        var employees = await _context.HrmEmployeeMasters
            .Include(e => e.Employment)
            .Include(e => e.Payroll)
            .Where(e => requests.Select(request => request.EmployeeId).Contains(e.Id))
            .ToListAsync(cancellationToken);

        foreach (var action in commands)
        {
            var requestId = action.RequestId;
            var request = requestsById.GetValueOrDefault(requestId);

            if (request is null)
            {
                result.Items.Add(Failure(requestId, "No permanency request awaiting approval was found."));
                continue;
            }


            var employee = employees.FirstOrDefault(e => e.Id == request.EmployeeId);
            if (employee?.Employment is null || employee.Payroll is null)
            {
                result.Items.Add(Failure(requestId, "Employment or payroll information is not available."));
                continue;
            }

            var approvalHistory = request.ForwardToMovementHR(action.ForwardedBy, action.Remarks);
            request.UpdatedBy = action.ForwardedBy;
            _context.HrmLearnerConfirmationApprovalHistories.Add(approvalHistory);

            // Apply permanency to the employee record.
            employee.Employment.ConfirmationDate = request.ProbationCompletedOn;
            employee.Employment.UpdatedBy = action.ForwardedBy;
            employee.Payroll.GrossSalary = request.StandardGrossSalary;
            employee.Payroll.UpdatedBy = action.ForwardedBy;

            result.Items.Add(Success(request));
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Finalize(result);
    }


    public async Task<LearnerConfirmationBatchResultDto> ForwardToMovementHRAsync(List<LearnerConfirmationsCommand> commands, CancellationToken cancellationToken)
    {
        var result = new LearnerConfirmationBatchResultDto { TotalRequested = commands.Count };

        var requestIds = commands
            .Select(request => request.RequestId)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        var requests = await _context.HrmLearnerConfirmationRequests
            .Where(r => requestIds.Contains(r.Id)
                        && r.Status.ToLower() == LearnerConfirmationStatus.ForwardedHR.ToString().ToLower())
            .ToListAsync(cancellationToken);

        var requestsById = requests.ToDictionary(request => request.Id, StringComparer.OrdinalIgnoreCase);

        var employees = await _context.HrmEmployeeMasters
            .Include(e => e.Employment)
            .Include(e => e.Payroll)
            .Where(e => requests.Select(request => request.EmployeeId).Contains(e.Id))
            .ToListAsync(cancellationToken);

        foreach (var action in commands)
        {
            var requestId = action.RequestId;
            var request = requestsById.GetValueOrDefault(requestId);

            if (request is null)
            {
                result.Items.Add(Failure(requestId, "No permanency request awaiting approval was found."));
                continue;
            }


            var employee = employees.FirstOrDefault(e => e.Id == request.EmployeeId);
            if (employee?.Employment is null || employee.Payroll is null)
            {
                result.Items.Add(Failure(requestId, "Employment or payroll information is not available."));
                continue;
            }

            var approvalHistory = request.ForwardToCEO(action.ForwardedBy, action.Remarks);
            request.UpdatedBy = action.ForwardedBy;
            _context.HrmLearnerConfirmationApprovalHistories.Add(approvalHistory);

            // Apply permanency to the employee record.
            employee.Employment.ConfirmationDate = request.ProbationCompletedOn;
            employee.Employment.UpdatedBy = action.ForwardedBy;
            employee.Payroll.GrossSalary = request.StandardGrossSalary;
            employee.Payroll.UpdatedBy = action.ForwardedBy;

            result.Items.Add(Success(request));
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Finalize(result);
    }

    public async Task<List<PendingLearnerConfirmationDto>> GetPendingAsync(string? status,
        CancellationToken cancellationToken = default)
    {
        status = string.IsNullOrEmpty(status) ? LearnerConfirmationStatus.Forwarded.ToString() : status;

        return await (
            from request in _context.HrmLearnerConfirmationRequests.AsNoTracking()
            join employee in _context.HrmEmployeeMasters.AsNoTracking()
                on request.EmployeeId equals employee.Id
            join employment in _context.HrmEmployeeEmployments.AsNoTracking()
                on employee.Id equals employment.EmployeeId
            join payroll in _context.HrmEmployeePayrolls.AsNoTracking()
                on employee.Id equals payroll.EmployeeId
            where request.Status.ToLower() == status.ToLower()
            orderby request.ProbationCompletedOn, employee.EmployeeCode
            select new PendingLearnerConfirmationDto
            {
                RequestId = request.Id,
                EmployeeId = request.EmployeeId,
                EmployeeCode = employee.EmployeeCode,
                EmployeeName = employee.EmployeeName,
                DepartmentName = employment.Department != null ? employment.Department.DepartmentName : string.Empty,
                Designation = employment.Designation != null ? employment.Designation.DesignationName : string.Empty,
                ProbationPeriod = employment.ProbationPeriod,
                DateOfJoining = request.DateOfJoining,
                ProbationCompletedOn = request.ProbationCompletedOn,
                CurrentGrossSalary = request.CurrentGrossSalary,
                StandardGrossSalary = request.StandardGrossSalary,
                AdjustmentAmount = request.AdjustmentAmount,
                CurrentBasicSalary = payroll.BasicSalary ?? 0m,
                AdjustedBasicSalary = (payroll.BasicSalary ?? 0m) + request.AdjustmentAmount,
                EffectiveFrom = request.ProbationCompletedOn,
                Status = request.Status,
                ForwardedBy = request.ForwardedBy,
                ForwardedOn = request.ForwardedOn
            }).ToListAsync(cancellationToken);
    }

    private async Task<decimal?> GetStandardWorkerGrossSalaryAsync(string? gradeId, CancellationToken cancellationToken)
        => await _context.MstGrades
            .AsNoTracking()
            .Where(d => gradeId != null && d.Id == gradeId)
            .Select(d => (decimal?)d.MinimumSalary)
            .FirstOrDefaultAsync(cancellationToken);

    private static LearnerConfirmationResultItemDto Failure(string requestId, string message)
        => new()
        {
            RequestId = requestId,
            Succeeded = false,
            Message = message
        };

    private static LearnerConfirmationResultItemDto Success(HrmLearnerConfirmationRequest request)
        => new()
        {
            RequestId = request.Id,
            Status = request.Status,
            Succeeded = true
        };

    private static LearnerConfirmationBatchResultDto Finalize(LearnerConfirmationBatchResultDto result)
    {
        result.SucceededCount = result.Items.Count(i => i.Succeeded);
        result.FailedCount = result.Items.Count(i => !i.Succeeded);
        return result;
    }
}
