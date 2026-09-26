using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using NZ.Attendance.Application.AttendanceExceptions.Commands.CreateAttendanceExceptions;
using NZ.Attendance.Application.AttendanceExceptions.Dto;
using NZ.Attendance.Application.Interfaces.Repositories;
using NZ.Attendance.Domain.Entities;
using NZ.Attendance.Domain.Enums;
using NZ.Attendance.Domain.Services;
using NZ.Attendance.Infrastructure.Persistence;
using NZ.HRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NZ.Attendance.Infrastructure.Repositories
{
    public class AttendanceExceptionRepository : IAttendanceExceptionRepository
    {
        private readonly AttendanceDbContext _context;
        private readonly AttendanceExceptionWorkflow _workflow;

        public AttendanceExceptionRepository(
            AttendanceDbContext context,
            AttendanceExceptionWorkflow workflow)
        {
            _context = context;
            _workflow = workflow;
        }

        public async Task<List<string>> CreateRangeAsync(
            CreateAttendanceExceptionsCommand command,
            CancellationToken cancellationToken = default)
        {
            var entities = new List<AttAttendanceException>(command.Items.Count);

            foreach (var item in command.Items)
            {
                var entity = new AttAttendanceException
                {
                    EmployeeId = item.EmployeeId,
                    AttendanceDate = item.AttendanceDate,
                    ExceptionType = item.ExceptionType,
                    Time = item.Time,
                    Severity = item.Severity,
                    Remarks = item.Remarks,
                    Status = AttendanceExceptionStatus.Pending,
                    CreatedBy = command.UserId,
                    UpdatedBy = command.UserId
                };

                if (command.SubmitImmediately)
                    _workflow.Submit(entity, command.UserId);

                entities.Add(entity);
            }

            await _context.AttAttendanceExceptions.AddRangeAsync(entities, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return entities.Select(e => e.Id).ToList();
        }

        public async Task<AttendanceExceptionDetailDto?> GetByIdAsync(
            string id,
            CancellationToken cancellationToken = default)
        {
            var entity = await _context.AttAttendanceExceptions
                .AsNoTracking()
                .Include(e => e.Employee)
                .Include(e => e.History)
                .FirstOrDefaultAsync(e => e.Id == id && e.IsActive, cancellationToken);

            if (entity == null) return null;

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

            var processedAttendance = await _context.AttProcessedAttendances
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.EmployeeId == entity.EmployeeId && a.AttendanceDate == entity.AttendanceDate, cancellationToken);

            MstShift? shift = null;
            if (!string.IsNullOrWhiteSpace(processedAttendance?.ShiftId))
            {
                shift = await _context.MstShifts
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.Id == processedAttendance.ShiftId, cancellationToken);
            }

            var latestForward = entity.History
                .Where(h => h.ToStatus == AttendanceExceptionStatus.Forwarded || h.ToStatus == AttendanceExceptionStatus.Submitted)
                .OrderByDescending(h => h.ActionOn)
                .FirstOrDefault();

            var workflowTransactionIds = await _context.WfWorkflowTransactions
                .AsNoTracking()
                .Where(t => t.ReferenceId == entity.Id)
                .OrderByDescending(t => t.CreatedOn)
                .Select(t => t.Id)
                .ToListAsync(cancellationToken);

            var attachments = workflowTransactionIds.Count == 0
                ? new List<AttendanceExceptionAttachmentDto>()
                : await _context.WfWorkflowAttachments
                    .AsNoTracking()
                    .Where(a => workflowTransactionIds.Contains(a.WorkflowTransactionId))
                    .OrderByDescending(a => a.UploadDate)
                    .Select(a => new AttendanceExceptionAttachmentDto
                    {
                        AttachmentId = a.Id,
                        FileName = a.FileName,
                        UploadedOn = a.UploadDate,
                        DownloadUrl = $"/attachments/{a.Id}"
                    })
                    .ToListAsync(cancellationToken);

            return new AttendanceExceptionDetailDto
            {
                RequestId = entity.Id,
                Status = entity.Status.ToString().ToUpperInvariant(),
                ExceptionDate = entity.AttendanceDate,
                Shift = shift?.ShiftName,
                Employee = new AttendanceExceptionEmployeeInfoDto
                {
                    EmployeeId = string.IsNullOrWhiteSpace(entity.Employee?.EmployeeCode) ? entity.EmployeeId : entity.Employee.EmployeeCode,
                    EmployeeName = entity.Employee?.EmployeeName ?? string.Empty,
                    Department = department?.DepartmentName,
                    Designation = designation?.DesignationName,
                    DateOfJoining = employment?.JoiningDate,
                    ReportingManager = reportingManager == null
                        ? null
                        : new AttendanceExceptionReportingManagerDto
                        {
                            EmployeeId = string.IsNullOrWhiteSpace(reportingManager.EmployeeCode) ? reportingManager.Id : reportingManager.EmployeeCode,
                            EmployeeName = reportingManager.EmployeeName
                        }
                },
                Workflow = new AttendanceExceptionWorkflowInfoDto
                {
                    ForwardedByDepartment = department?.DepartmentName,
                    ForwardedBySection = section?.SectionName,
                    ForwardedOn = latestForward?.ActionOn ?? entity.CreatedOn,
                    CurrentStatus = entity.Status.ToString().ToUpperInvariant()
                },
                ExceptionInformation = new AttendanceExceptionInformationDto
                {
                    ExceptionType = entity.ExceptionType ?? string.Empty,
                    ExceptionDate = entity.AttendanceDate,
                    ShiftName = shift?.ShiftName,
                    ShiftTime = BuildShiftTime(shift),
                    ScheduledInTime = shift?.StartTime.ToString("hh:mm tt"),
                    ActualInTime = processedAttendance?.ActualInTime?.ToString("hh:mm tt"),
                    ScheduledOutTime = shift?.EndTime.ToString("hh:mm tt"),
                    ActualOutTime = processedAttendance?.ActualOutTime?.ToString("hh:mm tt"),
                    ReasonProvided = entity.Remarks,
                    RemarksByFloor = latestForward?.Comments ?? entity.Remarks
                },
                Attachments = attachments
            };
        }

        public async Task<(List<AttendanceExceptionDto> Items, int Total)> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 20,
            string? employeeId = null,
            string? exceptionType = null,
            DateOnly? from = null,
            DateOnly? to = null,
            AttendanceExceptionStatus? status = null,
            CancellationToken cancellationToken = default)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 20;

            var query = _context.AttAttendanceExceptions
                .AsNoTracking()
                .Where(e => e.IsActive);

            if (!string.IsNullOrWhiteSpace(employeeId))
                query = query.Where(e => e.EmployeeId == employeeId);

            if (!string.IsNullOrWhiteSpace(exceptionType))
                query = query.Where(e => e.ExceptionType == exceptionType);

            if (from.HasValue)
                query = query.Where(e => e.AttendanceDate >= from.Value);

            if (to.HasValue)
                query = query.Where(e => e.AttendanceDate <= to.Value);

            if (status.HasValue)
                query = query.Where(e => e.Status == status.Value);

            var total = await query.CountAsync(cancellationToken);

            var entities = await query
                .Include(e => e.Employee)
                .Include(e => e.History)
                .OrderByDescending(e => e.AttendanceDate)
                .ThenBy(e => e.SortOrder)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            var items = new List<AttendanceExceptionDto>(entities.Count);
            foreach (var entity in entities)
            {
                var dto = new AttendanceExceptionDto();
                MapHeader(entity, dto);
                items.Add(dto);
            }

            return (items, total);
        }

        public async Task UpdateAsync(
            string id,
            string? exceptionType,
            string? severity,
            string? remarks,
            string userId,
            CancellationToken cancellationToken = default)
        {
            var entity = await GetTrackedAsync(id, cancellationToken);

            _workflow.EnsureEditable(entity);

            entity.ExceptionType = exceptionType;
            entity.Severity = severity;
            entity.Remarks = remarks;
            entity.UpdatedBy = userId;
            entity.UpdatedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(string id, string userId, CancellationToken cancellationToken = default)
        {
            var entity = await GetTrackedAsync(id, cancellationToken);

            if (entity.Status == AttendanceExceptionStatus.Submitted)
                throw new InvalidOperationException(
                    "A submitted attendance exception must be cancelled before it can be deleted.");

            entity.IsActive = false;
            entity.UpdatedBy = userId;
            entity.UpdatedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task ForwardAsync(string id, string userId, string? comments, CancellationToken cancellationToken = default)
        {
            var entity = await GetTrackedAsync(id, cancellationToken);
            _workflow.ForWard(entity, userId, comments);
            var newHistory = entity.History.LastOrDefault();
            if (newHistory != null)
            {
                await _context.AttAttendanceExceptionHistories.AddAsync(newHistory, cancellationToken);
            }
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Entry.ReloadAsync throws InvalidOperationException when the entity no longer exists in the database.
                throw new InvalidOperationException("The attendance exception no longer exists.", ex);
             
            }
        }

        public async Task ApproveAsync(string id, string reviewerId, string? comments, CancellationToken cancellationToken = default)
        {
            var entity = await GetTrackedAsync(id, cancellationToken);
            _workflow.Approve(entity, reviewerId, comments);
            var newHistory = entity.History.LastOrDefault();
            if (newHistory != null && _context.Entry(newHistory).State == EntityState.Detached)
            {
                await _context.AttAttendanceExceptionHistories.AddAsync(newHistory, cancellationToken);
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RejectAsync(string id, string reviewerId, string? comments, CancellationToken cancellationToken = default)
        {
            var entity = await GetTrackedAsync(id, cancellationToken);
            if (entity.Status == AttendanceExceptionStatus.Pending)
                _workflow.ReviewReject(entity, reviewerId, comments);
            else
                _workflow.Reject(entity, reviewerId, comments ?? string.Empty);
            var newHistory = entity.History.LastOrDefault();
            if (newHistory != null && _context.Entry(newHistory).State == EntityState.Detached)
            {
                await _context.AttAttendanceExceptionHistories.AddAsync(newHistory, cancellationToken);
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task CancelAsync(string id, string userId, string? comments, CancellationToken cancellationToken = default)
        {
            var entity = await GetTrackedAsync(id, cancellationToken);
            _workflow.Cancel(entity, userId, comments);
            var newHistory = entity.History.LastOrDefault();
            if (newHistory != null && _context.Entry(newHistory).State == EntityState.Detached)
            {
                await _context.AttAttendanceExceptionHistories.AddAsync(newHistory, cancellationToken);
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<HashSet<(string EmployeeId, DateOnly AttendanceDate)>> GetExistingKeysAsync(
            IEnumerable<string> employeeIds,
            IEnumerable<DateOnly> dates,
            CancellationToken cancellationToken = default)
        {
            var employeeIdList = employeeIds.ToList();
            var dateList = dates.ToList();

            var rows = await _context.AttAttendanceExceptions
                .AsNoTracking()
                .Where(e => e.IsActive
                            && employeeIdList.Contains(e.EmployeeId)
                            && dateList.Contains(e.AttendanceDate))
                .Select(e => new { e.EmployeeId, e.AttendanceDate })
                .ToListAsync(cancellationToken);

            return rows.Select(r => (r.EmployeeId, r.AttendanceDate)).ToHashSet();
        }

        private async Task<AttAttendanceException> GetTrackedAsync(string id, CancellationToken cancellationToken)
        {
            var entity = await _context.AttAttendanceExceptions
                .Include(e => e.History)
                .FirstOrDefaultAsync(e => e.Id == id && e.IsActive, cancellationToken);

            if (entity == null)
                throw new KeyNotFoundException($"Attendance exception {id} not found");

            return entity;
        }

        private static void MapHeader(AttAttendanceException entity, AttendanceExceptionDto dto)
        {
            dto.Id = entity.Id;
            dto.EmployeeId = entity.EmployeeId;
            dto.EmployeeCode = entity.Employee?.EmployeeCode;
            dto.EmployeeName = entity.Employee?.EmployeeName;
            dto.AttendanceDate = entity.AttendanceDate;
            dto.ExceptionType = entity.ExceptionType;
            dto.Severity = entity.Severity;
            dto.Remarks = entity.Remarks;
            dto.Status = entity.Status.ToString();
            dto.CreatedOn = entity.CreatedOn;

            var forwarded = entity.History
                .Where(h => h.ToStatus == AttendanceExceptionStatus.Submitted)
                .OrderByDescending(h => h.ActionOn)
                .FirstOrDefault();

            if (forwarded != null)
            {
                dto.ForwardedBy = forwarded.ActionBy;
                dto.ForwardedOn = forwarded.ActionOn;
            }

            var reviewed = entity.History
                .Where(h => h.ToStatus == AttendanceExceptionStatus.Approved
                         || h.ToStatus == AttendanceExceptionStatus.Rejected)
                .OrderByDescending(h => h.ActionOn)
                .FirstOrDefault();

            if (reviewed != null)
            {
                dto.ReviewedBy = reviewed.ActionBy;
                dto.ReviewedOn = reviewed.ActionOn;
                dto.ReviewRemarks = reviewed.Comments;
            }
        }

        private static string? BuildShiftTime(MstShift? shift)
        {
            if (shift == null)
                return null;

            return $"{shift.StartTime:hh:mm tt} - {shift.EndTime:hh:mm tt}";
        }

        public async Task ForwardToITAsync(string requestId, string processedBy, string? remarks, CancellationToken cancellationToken)
        {
            var entity = await GetTrackedAsync(requestId, cancellationToken);
            _workflow.ForWardToIT(entity, processedBy, remarks);
            var newHistory = entity.History.LastOrDefault();
            if (newHistory != null)
            {
                await _context.AttAttendanceExceptionHistories.AddAsync(newHistory, cancellationToken);
            }
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Entry.ReloadAsync throws InvalidOperationException when the entity no longer exists in the database.
                throw new InvalidOperationException("The attendance exception no longer exists.", ex);

            }
        }
    }
}
