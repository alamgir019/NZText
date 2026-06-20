using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Domain.Services;

public class AttendanceProcessingService
{
    private const int AttendanceBoundaryHour = 7;
    private const int MaxGroupingHours = 18;
    private const int FilterMinutes = 10;

    /// <summary>
    /// Processes raw punches for a single employee over a date range
    /// and produces AttProcessedAttendance records.
    /// Priority: Holiday > WeeklyOff > Absent > Present
    /// </summary>
    public List<AttProcessedAttendance> Process(
        string employeeId,
        IEnumerable<DateOnly> dateRange,
        IEnumerable<(DateOnly Date, TimeOnly Time)> rawPunches,
        HashSet<DateOnly> holidayDates,
        Dictionary<DateOnly, string> shiftRosterByDate,
        HashSet<DayOfWeek> weeklyOffDays)          // ← new parameter
    {
        // Step 1 – Group all raw punches by date as DateTime for easier arithmetic
        var grouped = rawPunches
            .GroupBy(p => p.Date)
            .ToDictionary(
                g => g.Key,
                g => g.OrderBy(p => p.Time)
                      .Select(p => p.Date.ToDateTime(p.Time))
                      .ToList());

        // Step 2 – Handle overnight shifts
        foreach (var dateKey in grouped.Keys.OrderBy(d => d).ToArray())
        {
            var punches      = grouped[dateKey];
            var earlyPunches = punches.Where(p => p.Hour < AttendanceBoundaryHour).ToList();

            if (earlyPunches.Count == 0) continue;

            var normalPunches = punches.Where(p => p.Hour >= AttendanceBoundaryHour).ToList();
            var prevDate      = dateKey.AddDays(-1);

            if (grouped.TryGetValue(prevDate, out var prevPunches) && prevPunches.Count > 0)
            {
                var spanHours = (earlyPunches.Max() - prevPunches.Min()).TotalHours;

                if (spanHours <= MaxGroupingHours)
                {
                    prevPunches.AddRange(earlyPunches);
                    prevPunches.Sort();
                    grouped[prevDate] = prevPunches;
                    grouped[dateKey]  = normalPunches;
                    continue;
                }
            }

            grouped[dateKey] = earlyPunches.Concat(normalPunches).OrderBy(p => p).ToList();
        }

        // Step 3 – Build one AttProcessedAttendance row per date in the range
        var result = new List<AttProcessedAttendance>();

        foreach (var date in dateRange.OrderBy(d => d))
        {
            var dayPunches = grouped.TryGetValue(date, out var gp)
                ? gp.OrderBy(p => p).ToList()
                : new List<DateTime>();

            var filtered = FilterClosePunches(dayPunches);
            shiftRosterByDate.TryGetValue(date, out var shiftId);

            // Priority 1 – Gazette / Public Holiday
            if (holidayDates.Contains(date))
            {
                result.Add(BuildRecord(employeeId, date, shiftId,
                    null, null, 0, "Holiday"));
                continue;
            }

            // Priority 2 – Weekly Off
            // If the employee punched in on their weekly off day, still record
            // the actual times but mark status as WeeklyOff.
            if (weeklyOffDays.Contains(date.DayOfWeek))
            {
                var woFirstIn = filtered.Count > 0 ? filtered.First() : (DateTime?)null;
                var woLastOut = filtered.Count > 1 ? filtered.Last()  : (DateTime?)null;
                var woHours   = woFirstIn.HasValue && woLastOut.HasValue
                    ? (decimal)(woLastOut.Value - woFirstIn.Value).TotalHours
                    : 0m;

                result.Add(BuildRecord(employeeId, date, shiftId,
                    woFirstIn, woLastOut, woHours, "WeeklyOff"));
                continue;
            }

            // Priority 3 – Absent
            if (filtered.Count == 0)
            {
                result.Add(BuildRecord(employeeId, date, shiftId,
                    null, null, 0, "Absent"));
                continue;
            }

            // Priority 4 – Present
            var firstIn     = filtered.First();
            var lastOut     = filtered.Last();
            var hasLogout   = filtered.Count > 1 && firstIn != lastOut;
            var workedHours = hasLogout
                ? (decimal)(lastOut - firstIn).TotalHours
                : 0m;

            result.Add(BuildRecord(
                employeeId, date, shiftId,
                firstIn,
                hasLogout ? lastOut : null,
                workedHours,
                "Present"));
        }

        return result;
    }

    private static AttProcessedAttendance BuildRecord(
        string employeeId,
        DateOnly date,
        string? shiftId,
        DateTime? inTime,
        DateTime? outTime,
        decimal workedHours,
        string status) => new()
    {
        EmployeeId       = employeeId,
        AttendanceDate   = date,
        ShiftId          = shiftId,
        ActualInTime     = inTime,
        ActualOutTime    = outTime,
        PayableInTime    = inTime,
        PayableOutTime   = outTime,
        WorkedHours      = workedHours,
        OtWorkedHours    = 0,
        OtPayableHours   = 0,
        AttendanceStatus = status,
        ProcessingStatus = "Processed",
        ProcessedDate    = DateTime.UtcNow
    };

    private static List<DateTime> FilterClosePunches(List<DateTime> punches)
    {
        if (punches.Count == 0) return punches;

        var filtered = new List<DateTime> { punches[0] };

        for (var i = 1; i < punches.Count; i++)
        {
            if ((punches[i] - filtered[^1]).TotalMinutes > FilterMinutes)
                filtered.Add(punches[i]);
        }

        return filtered;
    }
}
