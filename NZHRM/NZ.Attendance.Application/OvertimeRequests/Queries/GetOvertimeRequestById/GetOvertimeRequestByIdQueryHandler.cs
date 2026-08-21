using NZ.Attendance.Application.Interfaces.Repositories;

namespace NZ.Attendance.Application.OvertimeRequests.Queries.GetOvertimeRequestById
{
    public class GetOvertimeRequestByIdQueryHandler
    {
        private readonly IOvertimeRequestRepository _repository;

        public GetOvertimeRequestByIdQueryHandler(IOvertimeRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<Dto.OvertimeRequestDto?> Handle(GetOvertimeRequestByIdQuery query, CancellationToken cancellationToken = default)
        {
            return await _repository.GetByIdAsync(query.Id, cancellationToken);
        }
    }
}
