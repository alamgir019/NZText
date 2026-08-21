using NZ.Attendance.Application.Interfaces.Repositories;
using NZ.Attendance.Application.OvertimeRequests.Dto;

namespace NZ.Attendance.Application.OvertimeRequests.Queries.GetAllOvertimeRequests
{
    public class GetAllOvertimeRequestsQueryHandler
    {
        private readonly IOvertimeRequestRepository _repository;

        public GetAllOvertimeRequestsQueryHandler(IOvertimeRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<(List<OvertimeRequestDto> Items, int Total)> Handle(GetAllOvertimeRequestsQuery query, CancellationToken cancellationToken = default)
        {
            return await _repository.GetAllAsync(query.PageNumber, query.PageSize, query.ShiftId, query.DepartmentId, query.From, query.To, query.Status, cancellationToken);
        }
    }
}
