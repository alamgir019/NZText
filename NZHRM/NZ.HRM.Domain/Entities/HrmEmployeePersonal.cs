using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("employee_personal", Schema = "hrm")]
    public class HrmEmployeePersonal : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty; // FK to employee_master.Id

        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public string? SpouseName { get; set; }
        public string? Gender { get; set; }
        public string? Religion { get; set; }
        public string? MaritalStatus { get; set; }
        public string? BloodGroup { get; set; }
        public string? Nationality { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? NidNo { get; set; }
        public string? BirthCertificateNo { get; set; }
        public string? PassportNo { get; set; }
        public string? ReferenceType { get; set; }
        public string? EmployeeReference { get; set; }
        public string? ReferencePersonId { get; set; }
        public string? ReferenceMobileNumber { get; set; }
        public string? Relationship { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
    }
}
