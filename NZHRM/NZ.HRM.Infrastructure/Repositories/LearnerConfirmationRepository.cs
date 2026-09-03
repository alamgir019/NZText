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

        var standardGrossSalary = await GetStandardWorkerGrossSalaryAsync(cancellationToken);

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
                command.Remarks);

            request.CreatedBy = command.ForwardedBy;
            request.UpdatedBy = command.ForwardedBy;

            await _context.HrmLearnerConfirmationRequests.AddAsync(request, cancellationToken);

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
        var result = new LearnerConfirmationBatchResultDto { TotalRequested = command.EmployeeIds.Count };

        var requests = await _context.HrmLearnerConfirmationRequests
            .Where(r => command.EmployeeIds.Contains(r.EmployeeId)
                        && r.Status == LearnerConfirmationStatus.Forwarded.ToString())
            .ToListAsync(cancellationToken);

        var employeeIds = requests.Select(r => r.EmployeeId).ToList();

        var employees = await _context.HrmEmployeeMasters
            .Include(e => e.Employment)
            .Include(e => e.Payroll)
            .Where(e => employeeIds.Contains(e.Id))
            .ToListAsync(cancellationToken);

        foreach (var employeeId in command.EmployeeIds)
        {
            var request = requests.FirstOrDefault(r => r.EmployeeId == employeeId);

            if (request is null)
            {
                result.Items.Add(Failure(employeeId, "No permanency request awaiting approval was found."));
                continue;
            }

            if (!command.Approved)
            {
                request.Reject(command.ApprovedBy, command.Remarks);
                request.UpdatedBy = command.ApprovedBy;
                result.Items.Add(Success(employeeId, request));
                continue;
            }

            var employee = employees.FirstOrDefault(e => e.Id == employeeId);
            if (employee?.Employment is null || employee.Payroll is null)
            {
                result.Items.Add(Failure(employeeId, "Employment or payroll information is not available."));
                continue;
            }

            request.Approve(command.ApprovedBy, command.Remarks);
            request.UpdatedBy = command.ApprovedBy;

            // Apply permanency to the employee record.
            employee.Employment.ConfirmationDate = request.ProbationCompletedOn;
            employee.Employment.UpdatedBy = command.ApprovedBy;
            employee.Payroll.GrossSalary = request.StandardGrossSalary;
            employee.Payroll.UpdatedBy = command.ApprovedBy;

            result.Items.Add(Success(employeeId, request));
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Finalize(result);
    }

    public async Task<List<PendingLearnerConfirmationDto>> GetPendingAsync(
        CancellationToken cancellationToken = default)
    {
        var forwarded = LearnerConfirmationStatus.Forwarded.ToString();

        return await (
            from request in _context.HrmLearnerConfirmationRequests.AsNoTracking()
            join employee in _context.HrmEmployeeMasters.AsNoTracking()
                on request.EmployeeId equals employee.Id
            join employment in _context.HrmEmployeeEmployments.AsNoTracking()
                on employee.Id equals employment.EmployeeId
            where request.Status == forwarded
            orderby request.ProbationCompletedOn, employee.EmployeeCode
            select new PendingLearnerConfirmationDto
            {
                RequestId = request.Id,
                EmployeeId = request.EmployeeId,
                EmployeeCode = employee.EmployeeCode,
                EmployeeName = employee.EmployeeName,
                DepartmentName = employment.Department != null ? employment.Department.DepartmentName : string.Empty,
                Designation = employment.Designation != null ? employment.Designation.DesignationName : string.Empty,
                DateOfJoining = request.DateOfJoining,
                ProbationCompletedOn = request.ProbationCompletedOn,
                CurrentGrossSalary = request.CurrentGrossSalary,
                StandardGrossSalary = request.StandardGrossSalary,
                AdjustmentAmount = request.AdjustmentAmount,
                Status = request.Status,
                ForwardedBy = request.ForwardedBy,
                ForwardedOn = request.ForwardedOn
            }).ToListAsync(cancellationToken);
    }

    private async Task<decimal?> GetStandardWorkerGrossSalaryAsync(CancellationToken cancellationToken)
        => await _context.MstDesignations
            .AsNoTracking()
            .Where(d => d.DesignationName.ToLower() == ProbationAdjustmentPolicy.StandardWorkerDesignationName.ToLower()
                        && d.Grade != null)
            .Select(d => (decimal?)d.Grade!.MinimumSalary)
            .FirstOrDefaultAsync(cancellationToken);

    private static LearnerConfirmationResultItemDto Failure(string employeeId, string message)
        => new()
        {
            EmployeeId = employeeId,
            Succeeded = false,
            Message = message
        };

    private static LearnerConfirmationResultItemDto Success(string employeeId, HrmLearnerConfirmationRequest request)
        => new()
        {
            EmployeeId = employeeId,
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
