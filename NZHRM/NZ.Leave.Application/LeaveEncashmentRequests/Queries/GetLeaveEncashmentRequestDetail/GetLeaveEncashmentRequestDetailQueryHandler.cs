using NZ.Leave.Application.Interfaces.Repositories;
using NZ.Leave.Application.LeaveEncashmentRequests.Dto;

namespace NZ.Leave.Application.LeaveEncashmentRequests.Queries.GetLeaveEncashmentRequestDetail
{
    public class GetLeaveEncashmentRequestDetailQueryHandler
    {
        private readonly ILeaveEncashmentRequestRepository _repository;

        public GetLeaveEncashmentRequestDetailQueryHandler(ILeaveEncashmentRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<LeaveEncashmentRequestDetailDto?> Handle(GetLeaveEncashmentRequestDetailQuery query, CancellationToken cancellationToken = default)
        {
            if (query == null || string.IsNullOrWhiteSpace(query.RequestId))
                return null;

            return await _repository.GetDetailByIdAsync(query.RequestId, cancellationToken);
        }
    }
}