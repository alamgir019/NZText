using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("employee_nominee", Schema = "hrm")]
    public class HrmEmployeeNominee : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string? NomineeName { get; set; } = string.Empty;
        public string? Relationship { get; set; }
        public string? NomineeNameBangla { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? NidNo { get; set; }
        public string? MobileNo { get; set; }
        public string? Address { get; set; }
        public decimal NominationPercentage { get; set; }
        [ForeignKey("EmployeeId")]
        public HrmEmployeeMaster? Employee { get; set; }
    }
}
