using NZ.HRM.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZ.HRM.Domain.Entities
{
    public class RawPunch : BaseEntity
    {
        [Required]
        public string EmployeeId { get; set; } = string.Empty;
        [ForeignKey(nameof(EmployeeId))]
        public EmployeeMaster? Employee { get; set; }

        [Required]
        public DateTime PunchDate { get; set; }

        public TimeSpan? PunchTime { get; set; }

        [MaxLength(10)]
        public string? PunchType { get; set; } // "In" or "Out"

        [MaxLength(50)]
        public string? DeviceId { get; set; }

        [MaxLength(50)]
        public string? EmployeeCode { get; set; }
    }
}
