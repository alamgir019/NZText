using System;
using NZ.Attendance.Domain.Enums;

namespace NZ.Attendance.Application.AttendanceExceptions.Queries.GetAllAttendanceExceptions
{
    public class GetAllAttendanceExceptionsQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string? EmployeeId { get; set; }
        public string? ExceptionType { get; set; }
        public DateOnly? From { get; set; }
        public DateOnly? To { get; set; }
        public AttendanceExceptionStatus? Status { get; set; }
    }
}
