using System;
using System.Collections.Generic;

namespace NZ.Attendance.Application.OvertimeRequests.Dto
{
    public class OvertimeRequestDto
    {
        // Id uses string to align with BaseEntity Id type
        public string RequestId { get; set; } = string.Empty;
        public string CurrentShiftId { get; set; } = string.Empty;
        public DateTime OTDate { get; set; }
        public string DepartmentId { get; set; } = string.Empty;
        public string? Reason { get; set; }
        public List<OvertimeEmployeeDto> Employees { get; set; } = new List<OvertimeEmployeeDto>();
        public string EmployeeId { get; set; } = string.Empty;
        public string? EmployeeCode { get; set; }
        public string? EmployeeName { get; set; }
        public string? DepartmentName { get; set; }

        // OT hours in HH:mm format
        public string? OTHours { get; set; } = string.Empty;
        // Approval status at employee item level
        public string? Status { get; set; }
        // ItemId refers to AttOtRequestItem.Id when using single-table-per-employee design
        public string? ItemId { get; set; }
        public string? SubmittedBy { get; set; }
        public string? UnitId { get; set; }
    }
}
