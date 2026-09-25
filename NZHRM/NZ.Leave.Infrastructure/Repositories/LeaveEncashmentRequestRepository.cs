using Microsoft.EntityFrameworkCore;
using NZ.Leave.Application.Interfaces.Repositories;
using NZ.Shared.Contracts.Leave;
using NZ.Leave.Application.LeaveEncashmentRequests.Dto;
using NZ.Leave.Application.LeaveEncashmentRequests.Enums;
using NZ.Leave.Domain.Entities;
using NZ.Leave.Domain.Services;
using NZ.Leave.Infrastructure.Persistence;

namespace NZ.Leave.Infrastructure.Repositories
{
    public class LeaveEncashmentRequestRepository : ILeaveEncashmentRequestRepository
    {
        private readonly LeaveDbContext _context;
        private readonly ILeaveBalanceQuery _leaveBalanceQuery;
        private readonly LeaveEncashmentApprovalWorkflow _workflow;

        public LeaveEncashmentRequestRepository(
            LeaveDbContext context,
            ILeaveBalanceQuery leaveBalanceQuery,
            LeaveEncashmentApprovalWorkflow workflow)
        {
            _context = context;
            _leaveBalanceQuery = leaveBalanceQuery;
            _workflow = workflow;
        }

        public async Task<string> CreateAsync(LeaveEncashmentRequestDto dto, CancellationToken cancellationToken = default)
        {
            var leaveType = await _context.LevLeaveTypes
                .FirstOrDefaultAsync(lt => lt.LeaveCode == dto.LeaveType, cancellationToken);

            if (leaveType == null)
                throw new KeyNotFoundException($"Leave type {dto.LeaveType} not found");

            var entity = new LevLeaveEncashment
            {
                EmployeeId = dto.EmployeeId,
                LeaveTypeId = leaveType.Id,
                EncashDate = dto.EncashDate.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc),
                EncashDays = dto.EncashDays,
                Reason = dto.Reason,
                Instalment = dto.Instalment,
                Status = dto.Status,
                FromDate = dto.FromDate,
                ToDate = dto.ToDate,
                CreatedBy = dto.CreatedBy ?? string.Empty
            };

            _context.LevLeaveEncashments.Add(entity);
            // Create an initial encashment history record for the new encashment request
            var history = new LevLeaveEncashmentHistory
            {
                EncashmentId = entity.Id,
                WorkflowStepNo = 1,
                ApproverId = dto.CreatedBy,
                ActionTaken = "Submitted",
                Remarks = dto.Reason ?? string.Empty,
                CreatedBy = dto.CreatedBy ?? string.Empty
            };

            _context.LevLeaveEncashmentHistories.Add(history);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        public async Task<LeaveEncashmentRequestDto?> GetByIdAsync(string requestId, CancellationToken cancellationToken = default)
        {
            var entity = await _context.LevLeaveEncashments
                .AsNoTracking()
                .Include(a => a.Employee)
                .Include(a => a.LeaveType)
                .FirstOrDefaultAsync(a => a.Id == requestId, cancellationToken);

            if (entity == null) return null;

            // Get forwarded info from history (most recent history record)
            var history = await _context.LevLeaveEncashmentHistories
                .Where(h => h.EncashmentId == entity.Id)
                .OrderByDescending(h => h.CreatedOn)
                .FirstOrDefaultAsync(cancellationToken);

            // Get leave balance / accrued info for this employee and leave type
            var balances = await _leaveBalanceQuery.GetAllBalancesAsync(new List<string> { entity.EmployeeId }, cancellationToken);
            var balance = balances.FirstOrDefault(b => string.Equals(b.LeaveCode, entity.LeaveType?.LeaveCode, StringComparison.OrdinalIgnoreCase));

            var dto = Map(entity);
            dto.ForwardedBy = history?.ApproverId ?? history?.CreatedBy ?? dto.ForwardedBy;
            dto.ForwardedDate = history?.CreatedOn ?? dto.ForwardedDate;
            dto.LeaveBalance = balance?.ClosingBalance ?? 0m;
            dto.LeaveAccruedThisYear = balance?.EarnedLeaveAccrued ?? 0m;

            return dto;
        }

        public async Task<LeaveEncashmentRequestDetailDto?> GetDetailByIdAsync(string requestId, CancellationToken cancellationToken = default)
        {
            var entity = await _context.LevLeaveEncashments
                .AsNoTracking()
                .Include(a => a.Employee)
                .Include(a => a.LeaveType)
                .FirstOrDefaultAsync(a => a.Id == requestId, cancellationToken);

            if (entity == null)
                return null;

            var employment = await _context.HrmEmployeeEmployments
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EmployeeId == entity.EmployeeId, cancellationToken);

            var department = string.IsNullOrWhiteSpace(employment?.DepartmentId)
                ? null
                : await _context.MstDepartments
                    .AsNoTracking()
                    .FirstOrDefaultAsync(d => d.Id == employment.DepartmentId, cancellationToken);

            var section = string.IsNullOrWhiteSpace(employment?.SectionId)
                ? null
                : await _context.MstSections
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.Id == employment.SectionId, cancellationToken);

            var designation = string.IsNullOrWhiteSpace(employment?.DesignationId)
                ? null
                : await _context.MstDesignations
                    .AsNoTracking()
                    .FirstOrDefaultAsync(d => d.Id == employment.DesignationId, cancellationToken);

            var reportingManagerId = employment?.ReportingTo;
            if (string.IsNullOrWhiteSpace(reportingManagerId))
            {
                reportingManagerId = await _context.HrmEmployeeReportings
                    .AsNoTracking()
                    .Where(r => r.EmployeeId == entity.EmployeeId)
                    .OrderByDescending(r => r.EffectiveFrom)
                    .Select(r => r.ReportingEmployeeId)
                    .FirstOrDefaultAsync(cancellationToken);
            }

            var reportingManager = string.IsNullOrWhiteSpace(reportingManagerId)
                ? null
                : await _context.HrmEmployeeMasters
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.Id == reportingManagerId, cancellationToken);

            var histories = await _context.LevLeaveEncashmentHistories
                .AsNoTracking()
                .Where(h => h.EncashmentId == entity.Id)
                .OrderByDescending(h => h.CreatedOn)
                .ToListAsync(cancellationToken);

            var latestHistory = histories.FirstOrDefault();
            var latestForwardHistory = histories.FirstOrDefault(h =>
                string.Equals(h.ActionTaken, LeaveEncashmentRequestAction.Forward, StringComparison.OrdinalIgnoreCase)
                || string.Equals(h.ActionTaken, LeaveEncashmentRequestStatus.Forwarded, StringComparison.OrdinalIgnoreCase)
                || string.Equals(h.ActionTaken, "Submitted", StringComparison.OrdinalIgnoreCase));

            var balances = await _leaveBalanceQuery.GetAllBalancesAsync(new List<string> { entity.EmployeeId }, cancellationToken);
            var balance = balances.FirstOrDefault(b => string.Equals(b.LeaveCode, entity.LeaveType?.LeaveCode, StringComparison.OrdinalIgnoreCase));

            var workflowTransactionIds = await _context.WfWorkflowTransactions
                .AsNoTracking()
                .Where(t => t.ReferenceId == entity.Id)
                .OrderByDescending(t => t.CreatedOn)
                .Select(t => t.Id)
                .ToListAsync(cancellationToken);

            var attachments = workflowTransactionIds.Count == 0
                ? new List<LeaveEncashmentAttachmentDto>()
                : await _context.WfWorkflowAttachments
                    .AsNoTracking()
                    .Where(a => workflowTransactionIds.Contains(a.WorkflowTransactionId))
                    .OrderByDescending(a => a.UploadDate)
                    .Select(a => new LeaveEncashmentAttachmentDto
                    {
                        AttachmentId = a.Id,
                        FileName = a.FileName,
                        FileSizeKB = 0,
                        UploadedOn = a.UploadDate,
                        DownloadUrl = $"/attachments/{a.Id}"
                    })
                    .ToListAsync(cancellationToken);

            var requestType = MapRequestType(entity.LeaveType?.LeaveCode);
            var detail = new LeaveEncashmentRequestDetailDto
            {
                RequestId = entity.Id,
                RequestType = requestType,
                Status = entity.Status ?? string.Empty,
                AppliedOn = entity.CreatedOn,
                Employee = new LeaveEncashmentEmployeeDto
                {
                    EmployeeId = string.IsNullOrWhiteSpace(entity.Employee?.EmployeeCode) ? entity.EmployeeId : entity.Employee.EmployeeCode,
                    EmployeeName = entity.Employee?.EmployeeName ?? string.Empty,
                    Department = department?.DepartmentName,
                    Designation = designation?.DesignationName,
                    DateOfJoining = employment?.JoiningDate,
                    ReportingManager = reportingManager == null
                        ? null
                        : new LeaveEncashmentReportingManagerDto
                        {
                            EmployeeId = string.IsNullOrWhiteSpace(reportingManager.EmployeeCode) ? reportingManager.Id : reportingManager.EmployeeCode,
                            EmployeeName = reportingManager.EmployeeName
                        }
                },
                Workflow = new LeaveEncashmentWorkflowDto
                {
                    ForwardedByDepartment = department?.DepartmentName,
                    ForwardedBySection = section?.SectionName,
                    ForwardedOn = latestForwardHistory?.CreatedOn ?? latestHistory?.CreatedOn ?? entity.CreatedOn
                },
                EncashmentDetails = new LeaveEncashmentDetailsDto
                {
                    EncashmentDaysRequested = entity.EncashDays,
                    EncashmentRate = "As per Company Policy",
                    EstimatedAmount = entity.EncashAmount,
                    RemarksByEmployee = entity.Reason
                },
                Attachments = attachments,
                ImportantRules = BuildImportantRules(requestType)
            };

            if (string.Equals(requestType, LeaveEncashmentType.MaternityLeave, StringComparison.OrdinalIgnoreCase))
            {
                var maternityEntitlement = entity.FromDate.HasValue && entity.ToDate.HasValue
                    ? entity.ToDate.Value.DayNumber - entity.FromDate.Value.DayNumber + 1
                    : 128;

                detail.MaternityLeaveInfo = new LeaveEncashmentMaternityLeaveInfoDto
                {
                    RequestPart = string.IsNullOrWhiteSpace(entity.Instalment)
                        ? "PART_1_PRE_DELIVERY"
                        : entity.Instalment!.Trim().ToUpperInvariant(),
                    MaternityLeaveEntitlement = maternityEntitlement,
                    LeaveStructure = "64 Days Pre-Delivery + 64 Days Post-Delivery",
                    ExpectedDeliveryDate = null,
                    MaternityLeaveStartDate = entity.FromDate,
                    MaternityLeaveEndDate = entity.ToDate,
                    EncashmentDaysRequested = entity.EncashDays,
                    RemarksByEmployee = entity.Reason,
                    Note = "Part 2 can be requested after delivery."
                };
            }
            else
            {
                var totalCredited = balance?.EarnedLeaveAccrued ?? 0m;
                var balanceEarnedLeave = balance?.ClosingBalance ?? 0m;
                var utilized = Math.Max((balance?.EarnedLeave ?? totalCredited) - balanceEarnedLeave, 0m);

                detail.EarnedLeaveInfo = new LeaveEncashmentEarnedLeaveInfoDto
                {
                    TotalEarnedLeaveCredited = totalCredited,
                    EarnedLeaveUtilized = utilized,
                    BalanceEarnedLeave = balanceEarnedLeave,
                    EligibleForEncashment = Math.Min(balanceEarnedLeave, entity.EncashDays)
                };
            }

            return detail;
        }

        public async Task<(List<LeaveEncashmentRequestDto> Items, int Total)> GetAllAsync(
            string? status,
            string? instalment,
            string? leaveType,
            int page,
            int size,
            CancellationToken cancellationToken = default)
        {
            var query = _context.LevLeaveEncashments
                .Include(a => a.Employee)
                .ThenInclude(a => a.Employment != null ? a.Employment.Department : null)
                .Include(a => a.LeaveType)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(a => a.Status == status);

            if (!string.IsNullOrWhiteSpace(instalment))
                query = query.Where(a => a.Instalment == instalment);

            if (!string.IsNullOrWhiteSpace(leaveType))
                query = query.Where(a => a.LeaveType.LeaveCode == leaveType);

            var total = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderByDescending(a => a.CreatedOn)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(cancellationToken);

            var encashmentIds = items.Select(i => i.Id).ToList();
            var employeeIds = items.Select(i => i.EmployeeId).Distinct().ToList();

            // fetch latest history per encashment
            var histories = await _context.LevLeaveEncashmentHistories
                .Where(h => encashmentIds.Contains(h.EncashmentId))
                .OrderByDescending(h => h.CreatedOn)
                .ToListAsync(cancellationToken);

            // fetch leave balances for employees
            var balances = await _leaveBalanceQuery.GetAllBalancesAsync(employeeIds, cancellationToken);

            var result = new List<LeaveEncashmentRequestDto>(items.Count);

            foreach (var item in items)
            {
                var dto = Map(item);

                var hist = histories.FirstOrDefault(h => h.EncashmentId == item.Id);
                if (hist != null)
                {
                    dto.ForwardedBy = hist.ApproverId ?? hist.CreatedBy ?? dto.ForwardedBy;
                    dto.ForwardedDate = hist.CreatedOn;
                }

                var bal = balances.FirstOrDefault(b => b.EmployeeId == item.EmployeeId && string.Equals(b.LeaveCode, item.LeaveType?.LeaveCode, StringComparison.OrdinalIgnoreCase));
                if (bal != null)
                {
                    dto.LeaveBalance = bal.ClosingBalance;
                    dto.LeaveAccruedThisYear = bal.EarnedLeaveAccrued;
                }

                result.Add(dto);
            }

            return (result, total);
        }

        public async Task ProcessActionAsync(
            string requestId,
            string action,
            string? remarks,
            string processedBy,
            CancellationToken cancellationToken = default)
        {
            var entity = await _context.LevLeaveEncashments
                .FirstOrDefaultAsync(a => a.Id == requestId, cancellationToken);

            if (entity == null)
                throw new KeyNotFoundException($"Leave request {requestId} not found");

            var nextWorkflowStep = await _context.LevLeaveEncashmentHistories
                .Where(h => h.EncashmentId == entity.Id)
                .Select(h => (int?)h.WorkflowStepNo)
                .MaxAsync(cancellationToken) ?? 0;

            var history = _workflow.ApplyAction(
                entity,
                action,
                processedBy,
                remarks,
                nextWorkflowStep + 1);

            _context.LevLeaveEncashmentHistories.Add(history);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(LeaveEncashmentRequestDto dto, CancellationToken cancellationToken = default)
        {
            var entity = await _context.LevLeaveEncashments
                .FirstOrDefaultAsync(a => a.Id == dto.RequestId, cancellationToken);

            if (entity == null)
                throw new KeyNotFoundException($"Leave request {dto.RequestId} not found");

            var leaveType = await _context.LevLeaveTypes
                .FirstOrDefaultAsync(lt => lt.LeaveCode == dto.LeaveType, cancellationToken);

            if (leaveType == null)
                throw new KeyNotFoundException($"Leave type {dto.LeaveType} not found");

            entity.EmployeeId = dto.EmployeeId;
            entity.LeaveTypeId = leaveType.Id;
            entity.EncashDate = dto.EncashDate.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
            entity.EncashDays = dto.EncashDays;
            entity.Reason = dto.Reason;
            entity.UpdatedBy = dto.ModifiedBy ?? entity.UpdatedBy;
            entity.Status = dto.Status;
            // Create an initial encashment history record for the new encashment request
            var history = new LevLeaveEncashmentHistory
            {
                EncashmentId = entity.Id,
                WorkflowStepNo = 1,
                ApproverId = dto.CreatedBy,
                ActionTaken = dto.Status,
                Remarks = dto.Reason ?? string.Empty,
                CreatedBy = dto.CreatedBy ?? string.Empty
            };

            _context.LevLeaveEncashmentHistories.Add(history);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(string requestId, CancellationToken cancellationToken = default)
        {
            var entity = await _context.LevLeaveEncashments
                .FirstOrDefaultAsync(a => a.Id == requestId, cancellationToken);

            if (entity == null)
                return;

            _context.LevLeaveEncashments.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        private static LeaveEncashmentRequestDto Map(LevLeaveEncashment entity) => new LeaveEncashmentRequestDto
        {
            RequestId = entity.Id,
            EmployeeId = entity.EmployeeId,
            EmployeeCode = entity.Employee?.EmployeeCode ?? string.Empty,
            EmployeeName = entity.Employee?.EmployeeName ?? string.Empty,
            Department = entity.Employee?.Employment?.Department?.DepartmentName,
            LeaveType = entity.LeaveType?.LeaveCode ?? string.Empty,
            EncashDate = entity.EncashDate.HasValue ? DateOnly.FromDateTime(entity.EncashDate.Value) : default,
            EncashDays = entity.EncashDays,
            Reason = entity.Reason ?? string.Empty,
            Instalment = entity.Instalment ?? string.Empty,
            Status = entity.Status ?? string.Empty,
            FromDate = entity.FromDate,
            ToDate = entity.ToDate,
            ForwardedBy = entity.CreatedBy,
            ForwardedDate = entity.CreatedOn,
            LeaveBalance = 0m,
            LeaveAccruedThisYear = 0m,
        };

        private static string MapRequestType(string? leaveCode)
        {
            if (string.IsNullOrWhiteSpace(leaveCode))
                return string.Empty;

            var normalized = leaveCode.Trim().ToUpperInvariant();
            return normalized switch
            {
                "EL" or LeaveEncashmentType.EarnedLeave => LeaveEncashmentType.EarnedLeave,
                "ML" or LeaveEncashmentType.MaternityLeave => LeaveEncashmentType.MaternityLeave,
                _ when normalized.Contains("EARNED") => LeaveEncashmentType.EarnedLeave,
                _ when normalized.Contains("MATERNITY") => LeaveEncashmentType.MaternityLeave,
                _ => normalized
            };
        }

        private static List<string> BuildImportantRules(string requestType)
        {
            if (string.Equals(requestType, LeaveEncashmentType.MaternityLeave, StringComparison.OrdinalIgnoreCase))
            {
                return new List<string>
                {
                    "Maternity leave encashment must follow the approved entitlement structure.",
                    "Supporting workflow actions are audit logged for every approval step.",
                    "Final amount will be verified before payroll processing."
                };
            }

            return new List<string>
            {
                "Encashment is allowed only on current year's earned leave.",
                "Eligible encashment is based on the employee's available leave balance.",
                "Final amount will be calculated by payroll."
            };
        }
    }
}
