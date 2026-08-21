using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NZ.Attendance.Application.OvertimeRequests.Dto;

namespace NZ.Attendance.Application.OvertimeRequests.Commands.CreateOvertimeRequest
{
    public class CreateOvertimeRequestCommand
    {
        [Required]
        public string CurrentShiftId { get; set; } = string.Empty;

        [Required]
        public DateTime OTDate { get; set; }

        [Required]
        public string DepartmentId { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Reason { get; set; }

        [Required]
        public List<OvertimeEmployeeDto> Employees { get; set; } = new List<OvertimeEmployeeDto>();
    }
}
