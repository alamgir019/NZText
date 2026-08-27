using NZ.Leave.Application.Interfaces.Repositories;
using NZ.Leave.Application.LeaveRequests.Dto;

namespace NZ.Leave.Application.LeaveRequests.Queries.GetLeaveRequests
{
    public class GetLeaveRequestsQueryHandler
    {
        private readonly ILeaveRequestRepository _repository;

        public GetLeaveRequestsQueryHandler(ILeaveRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<(List<LeaveRequestDto> Items, int Total)> Handle(GetLeaveRequestsQuery query, CancellationToken cancellationToken = default)
        {
            return await _repository.GetAllAsync(query.Status, query.Page, query.Size, cancellationToken);
        }
    }
}
