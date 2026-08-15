using Microsoft.EntityFrameworkCore;
using NZ.Attendance.Infrastructure.Persistence;
using NZ.Shared.Contracts.Attendance;

namespace NZ.Attendance.Infrastructure.Services;

public class AttendanceSummaryQueryService : IAttendanceSummaryQuery
{
    private readonly AttendanceDbContext _context;

    public AttendanceSummaryQueryService(AttendanceDbContext context)
    {
        _context = context;
    }

    public async Task<AttendanceSummaryResult?> GetMonthlySummaryAsync(
        string employeeId,
        int year,
        int month,
        CancellationToken cancellationToken = default)
    {
        var from = new DateOnly(year, month, 1);
        var to = from.AddMonths(1).AddDays(-1);

        var records = await _context.AttProcessedAttendances
            .Where(a => a.EmployeeId == employeeId
                     && a.AttendanceDate >= from
                     && a.AttendanceDate <= to)
            .Select(a => new { a.AttendanceStatus, a.OtPayableHours })
            .ToListAsync(cancellationToken);

        if (!records.Any()) return null;

        var presentDays = records.Count(r => r.AttendanceStatus == "P" || r.AttendanceStatus == "Present");
        var absentDays = records.Count(r => r.AttendanceStatus == "A" || r.AttendanceStatus == "Absent");
        var lateDays = records.Count(r => r.AttendanceStatus == "L" || r.AttendanceStatus == "Late");
        var totalOt = records.Sum(r => r.OtPayableHours);

        return new AttendanceSummaryResult(
            employeeId,
            presentDays,
            absentDays,
            lateDays,
            totalOt,
            0m);
    }
}
