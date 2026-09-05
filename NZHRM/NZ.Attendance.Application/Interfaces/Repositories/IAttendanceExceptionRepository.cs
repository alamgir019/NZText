using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NZ.Attendance.Application.AttendanceExceptions.Commands.CreateAttendanceExceptions;
using NZ.Attendance.Application.AttendanceExceptions.Dto;
using NZ.Attendance.Domain.Enums;

namespace NZ.Attendance.Application.Interfaces.Repositories
{
    public interface IAttendanceExceptionRepository
    {
        /// <summary>Creates a batch of exceptions and returns the generated ids.</summary>
        Task<List<string>> CreateRangeAsync(
            CreateAttendanceExceptionsCommand command,
            CancellationToken cancellationToken = default);

        Task<AttendanceExceptionDetailDto?> GetByIdAsync(
            string id,
            CancellationToken cancellationToken = default);

        Task<(List<AttendanceExceptionDto> Items, int Total)> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 20,
            string? employeeId = null,
            string? exceptionType = null,
            DateOnly? from = null,
            DateOnly? to = null,
            AttendanceExceptionStatus? status = null,
            CancellationToken cancellationToken = default);

        Task UpdateAsync(
            string id,
            string? exceptionType,
            string? severity,
            string? remarks,
            string userId,
            CancellationToken cancellationToken = default);

        /// <summary>Soft delete (IsActive = false).</summary>
        Task DeleteAsync(string id, string userId, CancellationToken cancellationToken = default);

        Task SubmitAsync(string id, string userId, string? comments, CancellationToken cancellationToken = default);
        Task ApproveAsync(string id, string reviewerId, string? comments, CancellationToken cancellationToken = default);
        Task RejectAsync(string id, string reviewerId, string comments, CancellationToken cancellationToken = default);
        Task CancelAsync(string id, string userId, string? comments, CancellationToken cancellationToken = default);

        /// <summary>Returns employee/date pairs that already exist, to prevent duplicates on bulk create.</summary>
        Task<HashSet<(string EmployeeId, DateOnly AttendanceDate)>> GetExistingKeysAsync(
            IEnumerable<string> employeeIds,
            IEnumerable<DateOnly> dates,
            CancellationToken cancellationToken = default);
    }
}
