using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("employee_contact", Schema = "hrm")]
    public class HrmEmployeeContact : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty; // FK to employee_master.Id

        public string? MobileNo { get; set; }
        public string? EmergencyContactNo { get; set; }
        public string? PersonalEmail { get; set; }
        public string? PresentAddress { get; set; }
        public string? PermanentAddress { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
    }
}
