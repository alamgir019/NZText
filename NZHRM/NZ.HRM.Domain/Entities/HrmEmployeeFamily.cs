using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("employee_family", Schema = "hrm")]
    public class HrmEmployeeFamily : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string FamilyMemberName { get; set; } = string.Empty;
        public string Relationship { get; set; } = string.Empty;
        public DateOnly? DateOfBirth { get; set; }
        public string? Occupation { get; set; }
        public string? MobileNo { get; set; }
        public bool DependentFlag { get; set; }
        [ForeignKey("EmployeeId")]
        public HrmEmployeeMaster? Employee { get; set; }
    }
}
