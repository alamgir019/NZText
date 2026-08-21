using System;
using System.ComponentModel.DataAnnotations;

namespace NZ.Attendance.Application.OvertimeRequests.Commands.AddOvertimeEmployee
{
    public class AddOvertimeEmployeeCommand
    {
        [Required]
        // Align with BaseEntity string id
        public string OvertimeRequestId { get; set; } = string.Empty;

        [Required]
        public string EmployeeId { get; set; } = string.Empty;

        // OT hours in HH:mm format
        [Required]
        public string OTHours { get; set; } = string.Empty;
    }
}
