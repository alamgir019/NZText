using NZ.HRM.Domain.Common;
using NZ.HRM.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZ.HRM.Domain.Entities
{
    public class ProcessedPunch : BaseEntity
    {
        [Required]
        public string EmployeeId { get; set; } = string.Empty;
        [ForeignKey(nameof(EmployeeId))]
        public EmployeeMaster? Employee { get; set; }

        [Required]
        public string RawPunchId { get; set; } = string.Empty;
        [ForeignKey(nameof(RawPunchId))]
        public RawPunch? RawPunch { get; set; }

        public string? ShiftId { get; set; }
        [ForeignKey(nameof(ShiftId))]
        public Shift? Shift { get; set; }

        [Required]
        public DateTime PunchDate { get; set; }

        /// <summary>
        /// The original raw punch time from the device.
        /// </summary>
        public TimeSpan RawPunchTime { get; set; }

        /// <summary>
        /// The adjusted punch time after applying shift-snapping and randomization.
        /// </summary>
        public TimeSpan AdjustedPunchTime { get; set; }

        [MaxLength(10)]
        public string PunchType { get; set; } = string.Empty; // "In" or "Out"
    }
}
