using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("employee_education", Schema = "hrm")]
    public class HrmEmployeeEducation : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string? EducationLevelId { get; set; }
        public string? InstituteName { get; set; }
        public string? BoardUniversity { get; set; }
        public int? PassingYear { get; set; }
        public string? ResultGpa { get; set; }
        public string? MajorSubject { get; set; }
        public string? CertificateNo { get; set; }
        [ForeignKey("EmployeeId")]
        public HrmEmployeeMaster? Employee { get; set; }
    }
}
