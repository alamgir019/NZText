using System.Globalization;
using System.IO;
using Microsoft.EntityFrameworkCore;
using NZ.HRM.Domain.Entities;
using NZ.Payroll.Application.Interfaces.Repositories;
using NZ.Payroll.Application.PayrollExceptions.DTOs;
using NZ.Payroll.Application.PayrollExceptions.Queries;
using NZ.Payroll.Domain.Enums;
using NZ.Payroll.Infrastructure.Persistence;

namespace NZ.Payroll.Infrastructure.Repositories;

public class PayrollExceptionRepository : IPayrollExceptionRepository
{
    private const string SourceTableName = "payroll.payroll_exception";
    private const string ForwardingDepartment = "Attendance Cell";

    private readonly PayrollDbContext _context;

    public PayrollExceptionRepository(PayrollDbContext context)
    {
        _context = context;
    }

    public async Task<PayrollExceptionRequestsResponseDto> GetPayrollExceptionRequestsAsync(GetPayrollExceptionRequestsQuery query, CancellationToken cancellationToken = default)
    {
        var pageNumber = query.PageNumber > 0 ? query.PageNumber : 1;
        var pageSize = query.PageSize > 0 ? query.PageSize : 10;

        var requestQuery = BuildRequestQuery();
        requestQuery = ApplyFilters(requestQuery, query, includeStatusFilter: false);

        var summary = new PayrollExceptionRequestSummaryDto
        {
            TotalRequests = await requestQuery.CountAsync(cancellationToken),
            PendingWithMe = await requestQuery.CountAsync(x => x.Status == PayrollExceptionStatuses.Pending, cancellationToken),
            ForwardedToHoIT = await requestQuery.CountAsync(x => x.Status == PayrollExceptionStatuses.ForwardedToIT, cancellationToken),
            Rejected = await requestQuery.CountAsync(x => x.Status == PayrollExceptionStatuses.Rejected, cancellationToken)
        };

        var statusFilteredQuery = ApplyFilters(requestQuery, query, includeStatusFilter: true);
        var totalRecords = await statusFilteredQuery.CountAsync(cancellationToken);

        var rows = await statusFilteredQuery
            .OrderByDescending(x => x.SubmittedOn)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PayrollExceptionRequestsResponseDto
        {
            Summary = summary,
            Pagination = new PayrollExceptionPaginationDto
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = totalRecords == 0 ? 0 : (int)Math.Ceiling(totalRecords / (double)pageSize)
            },
            Items = rows.Select(MapItem).ToList()
        };
    }

    public Task<List<PayPayrollException>> GetByIdsAsync(IReadOnlyCollection<string> ids, CancellationToken cancellationToken = default)
    {
        return _context.PayPayrollExceptions
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<PayrollExceptionRequestDetailDto?> GetDetailByIdAsync(string requestId, CancellationToken cancellationToken = default)
    {
        var entity = await _context.PayPayrollExceptions
            .AsNoTracking()
            .Include(x => x.Employee)
            .FirstOrDefaultAsync(x => x.Id == requestId && x.IsActive, cancellationToken);

        if (entity == null)
        {
            return null;
        }

        var employment = await _context.HrmEmployeeEmployments
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.EmployeeId == entity.EmployeeId, cancellationToken);

        var department = string.IsNullOrWhiteSpace(employment?.DepartmentId)
            ? null
            : await _context.MstDepartments
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == employment.DepartmentId, cancellationToken);

        var designation = string.IsNullOrWhiteSpace(employment?.DesignationId)
            ? null
            : await _context.MstDesignations
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == employment.DesignationId, cancellationToken);

        var shift = string.IsNullOrWhiteSpace(employment?.ShiftId)
            ? null
            : await _context.MstShifts
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == employment.ShiftId, cancellationToken);

        var reportingManagerId = employment?.ReportingTo;
        if (string.IsNullOrWhiteSpace(reportingManagerId))
        {
            reportingManagerId = await _context.HrmEmployeeReportings
                .AsNoTracking()
                .Where(x => x.EmployeeId == entity.EmployeeId)
                .OrderByDescending(x => x.EffectiveFrom)
                .Select(x => x.ReportingEmployeeId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        var reportingManager = string.IsNullOrWhiteSpace(reportingManagerId)
            ? null
            : await _context.HrmEmployeeMasters
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == reportingManagerId, cancellationToken);

        var remarksByAttendanceCell = await _context.AudDataChanges
            .AsNoTracking()
            .Where(x => x.RecordId == entity.Id && x.TableName == SourceTableName && x.FieldName == "ForwardingRemarks")
            .OrderByDescending(x => x.ChangeDate)
            .Select(x => x.NewValue)
            .FirstOrDefaultAsync(cancellationToken);

        var workflowTransactionIds = await _context.WfWorkflowTransactions
            .AsNoTracking()
            .Where(x => x.ReferenceId == entity.Id)
            .OrderByDescending(x => x.CreatedOn)
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);

        var attachments = workflowTransactionIds.Count == 0
            ? new List<PayrollExceptionRequestAttachmentDto>()
            : (await _context.WfWorkflowAttachments
                .AsNoTracking()
                .Where(x => workflowTransactionIds.Contains(x.WorkflowTransactionId))
                .OrderByDescending(x => x.UploadDate)
                .ToListAsync(cancellationToken))
                .Select(MapAttachment)
                .ToList();

        return new PayrollExceptionRequestDetailDto
        {
            RequestId = entity.Id,
            Status = ToDisplayStatus(NormalizeStatus(entity.Status)),
            AdjustmentDate = DateOnly.FromDateTime(entity.CreatedOn),
            Shift = shift?.ShiftName,
            Employee = new PayrollExceptionRequestDetailEmployeeDto
            {
                EmployeeId = string.IsNullOrWhiteSpace(entity.Employee?.EmployeeCode) ? entity.EmployeeId : entity.Employee.EmployeeCode,
                EmployeeName = entity.Employee?.EmployeeName ?? string.Empty,
                Department = department?.DepartmentName,
                Designation = designation?.DesignationName,
                DateOfJoining = employment?.JoiningDate,
                ReportingManager = reportingManager == null
                    ? null
                    : new PayrollExceptionRequestReportingManagerDto
                    {
                        EmployeeId = string.IsNullOrWhiteSpace(reportingManager.EmployeeCode) ? reportingManager.Id : reportingManager.EmployeeCode,
                        Name = reportingManager.EmployeeName
                    }
            },
            Adjustment = new PayrollExceptionRequestAdjustmentDto
            {
                AdjustmentType = entity.ExceptionType,
                AdjustmentNature = entity.ExceptionDescription,
                OriginalOutPunch = null,
                CorrectedOutPunch = null,
                OtApplicable = null,
                OtType = null,
                OtHours = null,
                OtRate = null,
                ImpactOnPayroll = true,
                ReasonProvided = entity.ExceptionDescription,
                RemarksByAttendanceCell = remarksByAttendanceCell
            },
            ForwardedBy = entity.ResolvedDate.HasValue
                ? new PayrollExceptionRequestForwardedByDto
                {
                    Department = ForwardingDepartment,
                    ForwardedDateTime = entity.ResolvedDate
                }
                : null,
            Attachments = attachments
        };
    }

    public async Task SaveForwardingAsync(IReadOnlyCollection<PayPayrollException> requests, string processedBy, string? remarks, CancellationToken cancellationToken = default)
    {
        var auditRows = new List<AudDataChange>();
        var eventRows = new List<AudSystemEvent>();

        foreach (var request in requests)
        {
            auditRows.Add(CreateAuditRow(request.Id, "Status", PayrollExceptionStatuses.Pending, request.Status, processedBy));
            auditRows.Add(CreateAuditRow(request.Id, "ResolvedBy", null, request.ResolvedBy, processedBy));
            auditRows.Add(CreateAuditRow(request.Id, "ResolvedDate", null, request.ResolvedDate?.ToString("O"), processedBy));

            if (!string.IsNullOrWhiteSpace(remarks))
            {
                auditRows.Add(CreateAuditRow(request.Id, "ForwardingRemarks", null, remarks, processedBy));
            }

            eventRows.Add(new AudSystemEvent
            {
                EventType = "PAYROLL_EXCEPTION_FORWARDED_TO_IT",
                EventDateTime = DateTime.UtcNow,
                UserId = processedBy,
                EventDescription = $"Payroll exception request '{request.Id}' forwarded to Head Office IT.",
                CreatedBy = processedBy,
                UpdatedBy = processedBy
            });
        }

        if (auditRows.Count > 0)
        {
            await _context.AudDataChanges.AddRangeAsync(auditRows, cancellationToken);
        }

        if (eventRows.Count > 0)
        {
            await _context.AudSystemEvents.AddRangeAsync(eventRows, cancellationToken);
        }

        _context.PayPayrollExceptions.UpdateRange(requests);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private IQueryable<PayrollExceptionRequestRow> BuildRequestQuery()
    {
        return from payrollException in _context.PayPayrollExceptions.AsNoTracking()
               join employee in _context.HrmEmployeeMasters.AsNoTracking() on payrollException.EmployeeId equals employee.Id into employeeJoin
               from employee in employeeJoin.DefaultIfEmpty()
               join employment in _context.HrmEmployeeEmployments.AsNoTracking() on payrollException.EmployeeId equals employment.EmployeeId into employmentJoin
               from employment in employmentJoin.DefaultIfEmpty()
               join department in _context.MstDepartments.AsNoTracking() on employment.DepartmentId equals department.Id into departmentJoin
               from department in departmentJoin.DefaultIfEmpty()
               join shift in _context.MstShifts.AsNoTracking() on employment.ShiftId equals shift.Id into shiftJoin
               from shift in shiftJoin.DefaultIfEmpty()
               select new PayrollExceptionRequestRow
               {
                   RequestId = payrollException.Id,
                   AttendanceDate = DateOnly.FromDateTime(payrollException.CreatedOn),
                   EmployeeId = payrollException.EmployeeId,
                   EmployeeName = employee != null ? employee.EmployeeName : string.Empty,
                   Department = department != null ? department.DepartmentName : string.Empty,
                   AdjustmentType = payrollException.ExceptionType,
                   Shift = shift != null ? shift.ShiftCode : null,
                   ShiftStart = shift != null ? shift.StartTime : null,
                   ShiftEnd = shift != null ? shift.EndTime : null,
                   ImpactAmount = null,
                   SubmittedBy = payrollException.CreatedBy,
                   SubmittedOn = payrollException.CreatedOn,
                   Status = string.IsNullOrWhiteSpace(payrollException.Status)
                       ? PayrollExceptionStatuses.Pending
                       : payrollException.Status!.ToUpper()
               };
    }

    private static IQueryable<PayrollExceptionRequestRow> ApplyFilters(IQueryable<PayrollExceptionRequestRow> query, GetPayrollExceptionRequestsQuery criteria, bool includeStatusFilter)
    {
        if (!string.IsNullOrWhiteSpace(criteria.RequestId))
        {
            var requestId = criteria.RequestId.Trim();
            query = query.Where(x => EF.Functions.ILike(x.RequestId, $"%{requestId}%"));
        }

        if (!string.IsNullOrWhiteSpace(criteria.EmployeeId))
        {
            var employeeId = criteria.EmployeeId.Trim();
            query = query.Where(x => EF.Functions.ILike(x.EmployeeId, $"%{employeeId}%"));
        }

        if (!string.IsNullOrWhiteSpace(criteria.EmployeeName))
        {
            var employeeName = criteria.EmployeeName.Trim();
            query = query.Where(x => EF.Functions.ILike(x.EmployeeName, $"%{employeeName}%"));
        }

        if (!string.IsNullOrWhiteSpace(criteria.Department))
        {
            var department = criteria.Department.Trim();
            query = query.Where(x => EF.Functions.ILike(x.Department, $"%{department}%"));
        }

        if (!string.IsNullOrWhiteSpace(criteria.AdjustmentType))
        {
            var adjustmentType = criteria.AdjustmentType.Trim();
            query = query.Where(x => x.AdjustmentType != null && EF.Functions.ILike(x.AdjustmentType, $"%{adjustmentType}%"));
        }

        if (criteria.AttendanceDateFrom.HasValue)
        {
            query = query.Where(x => x.AttendanceDate >= criteria.AttendanceDateFrom.Value);
        }

        if (criteria.AttendanceDateTo.HasValue)
        {
            query = query.Where(x => x.AttendanceDate <= criteria.AttendanceDateTo.Value);
        }

        if (includeStatusFilter && !string.IsNullOrWhiteSpace(criteria.Status))
        {
            var status = criteria.Status.Trim().ToUpperInvariant();
            query = query.Where(x => x.Status == status);
        }

        return query;
    }

    private static PayrollExceptionRequestDto MapItem(PayrollExceptionRequestRow row)
    {
        return new PayrollExceptionRequestDto
        {
            RequestId = row.RequestId,
            AttendanceDate = row.AttendanceDate,
            Employee = new PayrollExceptionRequestEmployeeDto
            {
                EmployeeId = row.EmployeeId,
                EmployeeName = row.EmployeeName,
                Department = row.Department
            },
            AdjustmentType = row.AdjustmentType,
            Shift = row.Shift,
            ShiftTime = FormatShiftTime(row.ShiftStart, row.ShiftEnd),
            ImpactAmount = row.ImpactAmount,
            SubmittedBy = row.SubmittedBy,
            SubmittedOn = row.SubmittedOn,
            Status = row.Status
        };
    }

    private static string? FormatShiftTime(TimeOnly? start, TimeOnly? end)
    {
        if (!start.HasValue || !end.HasValue)
        {
            return null;
        }

        return string.Concat(
            start.Value.ToString("hh:mm tt", CultureInfo.InvariantCulture),
            " - ",
            end.Value.ToString("hh:mm tt", CultureInfo.InvariantCulture));
    }

    private static AudDataChange CreateAuditRow(string requestId, string fieldName, string? oldValue, string? newValue, string changedBy)
    {
        return new AudDataChange
        {
            TableName = SourceTableName,
            RecordId = requestId,
            FieldName = fieldName,
            OldValue = oldValue,
            NewValue = newValue,
            ChangedBy = changedBy,
            ChangeDate = DateTime.UtcNow,
            CreatedBy = changedBy,
            UpdatedBy = changedBy
        };
    }

    private static PayrollExceptionRequestAttachmentDto MapAttachment(WfWorkflowAttachment attachment)
    {
        var fileSizeKb = 0L;
        if (!string.IsNullOrWhiteSpace(attachment.FilePath) && File.Exists(attachment.FilePath))
        {
            fileSizeKb = new FileInfo(attachment.FilePath).Length / 1024;
        }

        return new PayrollExceptionRequestAttachmentDto
        {
            AttachmentId = attachment.Id,
            FileName = attachment.FileName,
            FileSizeKb = fileSizeKb,
            UploadedDateTime = attachment.UploadDate,
            DownloadUrl = $"/api/payroll-adjustments/attachments/{attachment.Id}"
        };
    }

    private static string NormalizeStatus(string? status)
        => string.IsNullOrWhiteSpace(status)
            ? PayrollExceptionStatuses.Pending
            : status.Trim().ToUpperInvariant();

    private static string ToDisplayStatus(string status)
    {
        var normalized = status.Replace("_", " ", StringComparison.Ordinal);
        return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(normalized.ToLowerInvariant());
    }

    private sealed class PayrollExceptionRequestRow
    {
        public string RequestId { get; init; } = string.Empty;
        public DateOnly AttendanceDate { get; init; }
        public string EmployeeId { get; init; } = string.Empty;
        public string EmployeeName { get; init; } = string.Empty;
        public string Department { get; init; } = string.Empty;
        public string? AdjustmentType { get; init; }
        public string? Shift { get; init; }
        public TimeOnly? ShiftStart { get; init; }
        public TimeOnly? ShiftEnd { get; init; }
        public decimal? ImpactAmount { get; init; }
        public string? SubmittedBy { get; init; }
        public DateTime SubmittedOn { get; init; }
        public string Status { get; init; } = string.Empty;
    }
}
