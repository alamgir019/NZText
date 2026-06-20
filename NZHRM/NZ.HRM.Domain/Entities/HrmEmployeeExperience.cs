using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("employee_experience", Schema = "hrm")]
    public class HrmEmployeeExperience : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string? CompanyName { get; set; }
        public string? Designation { get; set; }
        public DateOnly? JoiningDate { get; set; }
        public DateOnly? LeavingDate { get; set; }
        public decimal? LastSalary { get; set; }
        public string? Responsibilities { get; set; }
        public string? ReasonForLeaving { get; set; }
        [ForeignKey("EmployeeId")]
        public HrmEmployeeMaster? Employee { get; set; }
    }
}
