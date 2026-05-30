using NZ.HRM.Domain.Entities;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Domain.Services
{
    public class PunchProcessingService
    {
        private static readonly Random _random = new();

        /// <summary>
        /// Determines which shift a punch belongs to and returns the adjusted time.
        /// 
        /// Logic:
        /// - If punch is within EarlyEntryToleranceMinutes before shift start ? snap to shift start ± randomization.
        /// - If punch is within LateExitToleranceMinutes after shift end ? snap to shift end ± randomization.
        /// - Otherwise, return the raw punch time as-is.
        /// </summary>
        public (Shift? matchedShift, TimeSpan adjustedTime, string punchType) ProcessPunch(
            TimeSpan rawPunchTime, IEnumerable<Shift> shifts)
        {
            foreach (var shift in shifts)
            {
                var shiftStart = shift.StartTime.ToTimeSpan();
                var shiftEnd = shift.EndTime.ToTimeSpan();
                var earlyTolerance = TimeSpan.FromMinutes(shift.EarlyEntryToleranceMinutes);
                var lateTolerance = TimeSpan.FromMinutes(shift.LateExitToleranceMinutes);

                // Check if punch is an entry (within tolerance before shift start)
                var earliestEntry = shiftStart - earlyTolerance;
                if (rawPunchTime >= earliestEntry && rawPunchTime <= shiftStart + TimeSpan.FromMinutes(15))
                {
                    var adjusted = ApplyRandomization(shiftStart, shift.RandomizationMinutes);
                    return (shift, adjusted, "In");
                }

                // Check if punch is an exit (within tolerance after shift end)
                // Handle overnight shifts (e.g., 22:00 to 06:00)
                var latestExit = shiftEnd + lateTolerance;
                if (rawPunchTime >= shiftEnd - TimeSpan.FromMinutes(15) && rawPunchTime <= latestExit)
                {
                    var adjusted = ApplyRandomization(shiftEnd, shift.RandomizationMinutes);
                    return (shift, adjusted, "Out");
                }
            }

            // No shift matched – return raw time
            return (null, rawPunchTime, "Unknown");
        }

        private static TimeSpan ApplyRandomization(TimeSpan baseTime, int randomizationMinutes)
        {
            var offsetMinutes = _random.Next(-randomizationMinutes, randomizationMinutes + 1);
            var adjusted = baseTime + TimeSpan.FromMinutes(offsetMinutes);

            // Ensure non-negative
            if (adjusted < TimeSpan.Zero)
                adjusted += TimeSpan.FromHours(24);

            return adjusted;
        }
    }
}
