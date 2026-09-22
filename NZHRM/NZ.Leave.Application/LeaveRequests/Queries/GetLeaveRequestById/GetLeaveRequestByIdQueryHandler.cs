using NZ.Leave.Application.Interfaces.Repositories;
using NZ.Leave.Application.LeaveRequests.Dto;

namespace NZ.Leave.Application.LeaveRequests.Queries.GetLeaveRequestById
{
    public class GetLeaveRequestByIdQueryHandler
    {
        private readonly ILeaveRequestRepository _repository;

        public GetLeaveRequestByIdQueryHandler(ILeaveRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<LeaveRequestDto?> Handle(GetLeaveRequestByIdQuery query, CancellationToken cancellationToken = default)
        {
            if (query == null || string.IsNullOrWhiteSpace(query.RequestId))
                return null;

            return await _repository.GetByIdAsync(query.RequestId, cancellationToken);
        }
    }
}
