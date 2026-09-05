using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NZ.Attendance.Application.AttendanceExceptions.Commands.CreateAttendanceExceptions;
using NZ.Attendance.Application.AttendanceExceptions.Dto;
using NZ.Attendance.Application.Interfaces.Repositories;
using NZ.Attendance.Domain.Entities;
using NZ.Attendance.Domain.Enums;
using NZ.Attendance.Domain.Services;
using NZ.Attendance.Infrastructure.Persistence;

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
                    Severity = item.Severity,
                    Remarks = item.Remarks,
                    Status = AttendanceExceptionStatus.Draft,
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

            var dto = new AttendanceExceptionDetailDto();
            MapHeader(entity, dto);

            dto.History = entity.History
                .OrderBy(h => h.ActionOn)
                .Select(h => new AttendanceExceptionHistoryDto
                {
                    FromStatus = h.FromStatus,
                    ToStatus = h.ToStatus,
                    ActionBy = h.ActionBy,
                    ActionOn = h.ActionOn,
                    Comments = h.Comments
                })
                .ToList();

            return dto;
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

        public async Task SubmitAsync(string id, string userId, string? comments, CancellationToken cancellationToken = default)
        {
            var entity = await GetTrackedAsync(id, cancellationToken);
            _workflow.Submit(entity, userId, comments);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task ApproveAsync(string id, string reviewerId, string? comments, CancellationToken cancellationToken = default)
        {
            var entity = await GetTrackedAsync(id, cancellationToken);
            _workflow.Approve(entity, reviewerId, comments);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RejectAsync(string id, string reviewerId, string comments, CancellationToken cancellationToken = default)
        {
            var entity = await GetTrackedAsync(id, cancellationToken);
            _workflow.Reject(entity, reviewerId, comments);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task CancelAsync(string id, string userId, string? comments, CancellationToken cancellationToken = default)
        {
            var entity = await GetTrackedAsync(id, cancellationToken);
            _workflow.Cancel(entity, userId, comments);
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
            dto.Status = entity.Status;
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
    }
}
