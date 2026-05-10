using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    public class EmployeeVerification : BaseEntity
    {
        [Required]
        [ForeignKey(nameof(Employee))]
        public string EmployeeId { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? SecurityClearanceBy { get; set; }

        public DateTime? SecurityClearanceDate { get; set; }

        [MaxLength(100)]
        public string? EnrolledBy { get; set; }

        public DateTime? EnrolledDate { get; set; }

        [MaxLength(100)]
        public string? BiometricEnrolledBy { get; set; }

        public DateTime? BiometricEnrolledDate { get; set; }

        public EmployeeMaster? Employee { get; set; }
    }
}
