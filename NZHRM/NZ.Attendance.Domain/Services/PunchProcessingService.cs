using NZ.HRM.Domain.Entities;

namespace NZ.Attendance.Domain.Services
{
    public class PunchProcessingService
    {
        private static readonly Random _random = new();
        const int RandomizationMinutes = 5;
        /// <summary>
        /// Determines which shift a punch belongs to and returns the adjusted time.
        /// 
        /// Logic:
        /// - If punch is within EarlyEntryToleranceMinutes before shift start ? snap to shift start ± randomization.
        /// - If punch is within LateExitToleranceMinutes after shift end ? snap to shift end ± randomization.
        /// - Otherwise, return the raw punch time as-is.
        /// </summary>
        public (MstShift? matchedShift, TimeOnly adjustedTime, string punchType) ProcessPunch(
            TimeOnly rawPunchTime, IEnumerable<MstShift> shifts)
        {
            foreach (var shift in shifts)
            {
                var shiftStart = shift.StartTime;
                var shiftEnd = shift.EndTime;
                
                // Keep these as TimeSpan, not TimeOnly
                var earlyTolerance = TimeSpan.FromMinutes(shift.GraceMinutes);
                var lateTolerance = TimeSpan.FromMinutes(shift.GraceMinutes);

                // Check if punch is an entry (within tolerance before shift start)
                var earliestEntry = shiftStart.Add(-earlyTolerance);
                if (rawPunchTime >= earliestEntry && rawPunchTime <= shiftStart.AddMinutes(15))
                {
                    var adjusted = ApplyRandomization(shiftStart, RandomizationMinutes);
                    return (shift, adjusted, "In");
                }

                // Check if punch is an exit (within tolerance after shift end)
                // Handle overnight shifts (e.g., 22:00 to 06:00)
                var latestExit = shiftEnd.Add(lateTolerance);
                if (rawPunchTime >= shiftEnd.AddMinutes(-15) && rawPunchTime <= latestExit)
                {
                    var adjusted = ApplyRandomization(shiftEnd, RandomizationMinutes);
                    return (shift, adjusted, "Out");
                }
            }

            // No shift matched – return raw time
            return (null, rawPunchTime, "Unknown");
        }

        private static TimeOnly ApplyRandomization(TimeOnly baseTime, int randomizationMinutes)
        {
            var offsetMinutes = _random.Next(-randomizationMinutes, randomizationMinutes + 1);
            var adjusted = baseTime.AddMinutes(offsetMinutes);

            return adjusted;
        }
    }
}
