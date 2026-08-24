using NZ.Leave.Application.LeaveTypes.Dto;

namespace NZ.Leave.Application.Interfaces.Repositories
{
    public interface ILeaveTypeRepository
    {
        Task<List<LeaveTypeDto>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
