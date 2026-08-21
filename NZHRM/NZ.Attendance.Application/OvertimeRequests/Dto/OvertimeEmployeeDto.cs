using System;

namespace NZ.Attendance.Application.OvertimeRequests.Dto
{
    public class OvertimeEmployeeDto
    {
        // Employee Id uses string type to match HrmEmployeeMaster.Id
        public string EmployeeId { get; set; } = string.Empty;
        public string? EmployeeCode { get; set; }
        public string? EmployeeName { get; set; }
        // OT hours in HH:mm format
        public string? OTHours { get; set; } = string.Empty;
        // Approval status at employee item level
        public string? Status { get; set; }
        // ItemId refers to AttOtRequestItem.Id when using single-table-per-employee design
        public string? ItemId { get; set; }
        public string? SubmittedBy { get; set; }
    }
}
