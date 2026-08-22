using NZ.Attendance.Application.Interfaces.Repositories;
using NZ.Attendance.Application.OvertimeRequests.Dto;

namespace NZ.Attendance.Application.OvertimeRequests.Queries.GetEmployeesByShift
{
    public class GetEmployeesByShiftQueryHandler
    {
        private readonly IOvertimeRequestRepository _repository;

        public GetEmployeesByShiftQueryHandler(IOvertimeRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<EmployeeByShiftDto>> Handle(GetEmployeesByShiftAndDepartmentQuery query, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(query.ShiftId))
                throw new ArgumentException("ShiftId is required");

            if (string.IsNullOrWhiteSpace(query.DepartmentId))
                throw new ArgumentException("DepartmentId is required");

            return await _repository.GetEmployeesByShiftAndDepartmentAsync(query.ShiftId, query.DepartmentId, cancellationToken);
        }
    }
}
