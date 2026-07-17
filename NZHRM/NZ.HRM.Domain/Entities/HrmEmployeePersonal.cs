using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;
using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Domain.Entities
{
    [Table("employee_personal", Schema = "hrm")]
    public class HrmEmployeePersonal : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty; // FK to employee_master.Id

        public string? FatherName { get; set; }
        public string? FatherNameBangla { get; set; }
        public string? MotherName { get; set; }
        public string? MotherNameBangla { get; set; }
        public string? GuardianNameBangla { get; set; }
        public GuardianType? GuardianType { get; set; }
        public string? SpouseName { get; set; }
        public string? Gender { get; set; }
        public string? Religion { get; set; }
        public string? MaritalStatus { get; set; }
        public string? BloodGroup { get; set; }
        public string? Nationality { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public IDType? IdType { get; set; }
        public string? IdNumber { get; set; }
        public string? BirthCertificateNo { get; set; }
        public string? PassportNo { get; set; }
        public string? EmployeeReference { get; set; }
        public string? ReferenceMobileNumber { get; set; }
        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
        public string? PermanentVillageAreaRoad { get; set; }
        public string? PermanentPostOffice { get; set; }
        public string? PermanentThanaId { get; set; }
        public string? PermanentDistrictId { get; set; }
        public string? PermanentDivisionId { get; set; }
        [ForeignKey(nameof(PermanentThanaId))]
        public LookThana? PermanentThana { get; set; }
        [ForeignKey(nameof(PermanentDistrictId))]
        public LookDistrict? PermanentDistrict { get; set; }
        [ForeignKey(nameof(PermanentDivisionId))]
        public LookDivision? PermanentDivision { get; set; }
        public string? PresentVillageAreaRoad { get; set; }
        public string? PresentPostOffice { get; set; }
        public string? PresentThanaId { get; set; }
        public string? PresentDistrictId { get; set; }
        public string? PresentDivisionId { get; set; }
        [ForeignKey(nameof(PresentThanaId))]
        public LookThana? PresentThana { get; set; }
        [ForeignKey(nameof(PresentDistrictId))]
        public LookDistrict? PresentDistrict { get; set; }
        [ForeignKey(nameof(PresentDivisionId))]
        public LookDivision? PresentDivision { get; set; }
        public string? MobileNumber { get; set; }
    }
}
