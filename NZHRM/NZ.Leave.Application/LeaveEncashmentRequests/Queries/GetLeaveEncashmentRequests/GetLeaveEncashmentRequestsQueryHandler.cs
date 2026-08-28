using NZ.Leave.Application.Interfaces.Repositories;
using NZ.Leave.Application.LeaveEncashmentRequests.Dto;

namespace NZ.Leave.Application.LeaveEncashmentRequests.Queries.GetLeaveEncashmentRequests
{
    public class GetLeaveEncashmentRequestsQueryHandler
    {
        private readonly ILeaveEncashmentRequestRepository _repository;

        public GetLeaveEncashmentRequestsQueryHandler(ILeaveEncashmentRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<(List<LeaveEncashmentRequestDto> Items, int Total)> Handle(GetLeaveEncashmentRequestsQuery query, CancellationToken cancellationToken = default)
        {
            return await _repository.GetAllAsync(query.Status, query.Page, query.Size, cancellationToken);
        }
    }
}
