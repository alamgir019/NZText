using System.Threading;
using System.Threading.Tasks;
using NZ.Attendance.Application.AttendanceExceptions.Dto;
using NZ.Attendance.Application.Interfaces.Repositories;

namespace NZ.Attendance.Application.AttendanceExceptions.Queries.GetAttendanceExceptionById
{
    public class GetAttendanceExceptionByIdQueryHandler
    {
        private readonly IAttendanceExceptionRepository _repository;

        public GetAttendanceExceptionByIdQueryHandler(IAttendanceExceptionRepository repository)
        {
            _repository = repository;
        }

        public async Task<AttendanceExceptionDetailDto?> Handle(
            GetAttendanceExceptionByIdQuery query,
            CancellationToken cancellationToken = default)
        {
            return await _repository.GetByIdAsync(query.Id, cancellationToken);
        }
    }
}
