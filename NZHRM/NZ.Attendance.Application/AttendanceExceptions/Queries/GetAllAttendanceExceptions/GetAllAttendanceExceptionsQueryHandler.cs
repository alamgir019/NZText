using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NZ.Attendance.Application.AttendanceExceptions.Dto;
using NZ.Attendance.Application.Interfaces.Repositories;

namespace NZ.Attendance.Application.AttendanceExceptions.Queries.GetAllAttendanceExceptions
{
    public class GetAllAttendanceExceptionsQueryHandler
    {
        private readonly IAttendanceExceptionRepository _repository;

        public GetAllAttendanceExceptionsQueryHandler(IAttendanceExceptionRepository repository)
        {
            _repository = repository;
        }

        public async Task<(List<AttendanceExceptionDto> Items, int Total)> Handle(
            GetAllAttendanceExceptionsQuery query,
            CancellationToken cancellationToken = default)
        {
            return await _repository.GetAllAsync(
                query.PageNumber, query.PageSize, query.EmployeeId, query.ExceptionType,
                query.From, query.To, query.Status, cancellationToken);
        }
    }
}
