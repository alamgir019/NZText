using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    public class Shift : BaseEntityWithSortOrder
    {
        public string ShiftName { get; set; } = string.Empty;
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        /// <summary>
        /// Tolerance in minutes before shift start to snap punch to shift start (e.g., 30 means punches within 30 min before start are snapped).
        /// </summary>
        public int EarlyEntryToleranceMinutes { get; set; } = 30;

        /// <summary>
        /// Tolerance in minutes after shift end to snap punch to shift end (e.g., 40 means punches within 40 min after end are snapped).
        /// </summary>
        public int LateExitToleranceMinutes { get; set; } = 40;

        /// <summary>
        /// Maximum randomization offset in minutes applied to adjusted time (e.g., 5 means ±5 minutes).
        /// </summary>
        public int RandomizationMinutes { get; set; } = 5;
    }
}
