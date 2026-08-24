using NZ.Leave.Application.LeaveRequests.Dto;

namespace NZ.Leave.Application.Interfaces.Repositories
{
    public interface ILeaveRequestRepository
    {
        Task<string> CreateAsync(LeaveRequestDto dto, CancellationToken cancellationToken = default);
        Task<LeaveRequestDto?> GetByIdAsync(string requestId, CancellationToken cancellationToken = default);
        Task<(List<LeaveRequestDto> Items, int Total)> GetAllAsync(
            string? status,
            int page,
            int size,
            CancellationToken cancellationToken = default);
        Task UpdateAsync(LeaveRequestDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(string requestId, CancellationToken cancellationToken = default);
        Task<bool> ExistsForEmployeeAsync(string employeeId, DateOnly fromDate, DateOnly toDate, string? excludeRequestId = null, CancellationToken cancellationToken = default);
    }
}
