using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("employee_verification", Schema = "hrm")]
    public class HrmEmployeeVerification : BaseEntity
    {
        [Required]
        public string EmployeeId { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? SecurityClearanceBy { get; set; }

        public DateOnly? SecurityClearanceDate { get; set; }

        [MaxLength(100)]
        public string? EnrolledBy { get; set; }

        public DateOnly? EnrolledDate { get; set; }

        [MaxLength(100)]
        public string? BiometricEnrolledBy { get; set; }

        public DateOnly? BiometricEnrolledDate { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
    }
}
