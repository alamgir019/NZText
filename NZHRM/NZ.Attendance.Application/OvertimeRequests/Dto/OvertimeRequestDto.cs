using System;
using System.Collections.Generic;

namespace NZ.Attendance.Application.OvertimeRequests.Dto
{
    public class OvertimeRequestDto
    {
        // Id uses string to align with BaseEntity Id type
        public string Id { get; set; } = string.Empty;
        public string CurrentShiftId { get; set; } = string.Empty;
        public DateTime OTDate { get; set; }
        public string DepartmentId { get; set; } = string.Empty;
        public string? Reason { get; set; }
        public List<OvertimeEmployeeDto> Employees { get; set; } = new List<OvertimeEmployeeDto>();
    }
}
