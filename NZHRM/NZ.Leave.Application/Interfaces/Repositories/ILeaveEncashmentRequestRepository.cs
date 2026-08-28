using NZ.Leave.Application.LeaveEncashmentRequests.Dto;

namespace NZ.Leave.Application.Interfaces.Repositories
{
    public interface ILeaveEncashmentRequestRepository
    {
        Task<string> CreateAsync(LeaveEncashmentRequestDto dto, CancellationToken cancellationToken = default);
        Task<LeaveEncashmentRequestDto?> GetByIdAsync(string requestId, CancellationToken cancellationToken = default);
        Task<(List<LeaveEncashmentRequestDto> Items, int Total)> GetAllAsync(
            string? status,
            int page,
            int size,
            CancellationToken cancellationToken = default);
        Task UpdateAsync(LeaveEncashmentRequestDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(string requestId, CancellationToken cancellationToken = default);
    }
}
