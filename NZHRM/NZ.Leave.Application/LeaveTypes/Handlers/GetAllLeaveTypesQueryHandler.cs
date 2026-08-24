using NZ.Leave.Application.Interfaces.Repositories;
using NZ.Leave.Application.LeaveTypes.Dto;
using NZ.Leave.Application.LeaveTypes.Queries.GetAllLeaveTypes;

namespace NZ.Leave.Application.LeaveTypes.Handlers
{
    public class GetAllLeaveTypesQueryHandler
    {
        private readonly ILeaveTypeRepository _repository;

        public GetAllLeaveTypesQueryHandler(ILeaveTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<LeaveTypeDto>> Handle(GetAllLeaveTypesQuery query, CancellationToken cancellationToken = default)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }
    }
}
