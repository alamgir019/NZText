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
        public string? PresentDivisionId { get; set; }
        public string? PresentDistrictId { get; set; }
        public string? PresentUpazilaId { get; set; }
        public string? PresentPostOffice { get; set; }
        public string? PresentVillage { get; set; }
        public string? PermanentDivisionId { get; set; }
        public string? PermanentDistrictId { get; set; }
        public string? PermanentUpazilaId { get; set; }
        public string? PermanentVillage { get; set; }
        public string? PermanentPostOffice { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
    }
}
