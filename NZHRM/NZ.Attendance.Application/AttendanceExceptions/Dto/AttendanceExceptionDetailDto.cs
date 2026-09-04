using System;
using System.Collections.Generic;
using NZ.Attendance.Domain.Enums;

namespace NZ.Attendance.Application.AttendanceExceptions.Dto
{
    public class AttendanceExceptionHistoryDto
    {
        public AttendanceExceptionStatus FromStatus { get; set; }
        public AttendanceExceptionStatus ToStatus { get; set; }
        public string ActionBy { get; set; } = string.Empty;
        public DateTime ActionOn { get; set; }
        public string? Comments { get; set; }
    }

    public class AttendanceExceptionDetailDto : AttendanceExceptionDto
    {
        public List<AttendanceExceptionHistoryDto> History { get; set; } = new();
    }
}
