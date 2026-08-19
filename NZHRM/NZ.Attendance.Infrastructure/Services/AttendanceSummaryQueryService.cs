using Microsoft.EntityFrameworkCore;
using NZ.Attendance.Infrastructure.Persistence;
using NZ.Shared.Contracts.Attendance;
using NZ.Attendance.Infrastructure.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace NZ.Attendance.Infrastructure.Services;

public class AttendanceSummaryQueryService : IAttendanceSummaryQuery, NZ.Attendance.Infrastructure.Contracts.IAttendanceDashboardQuery
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

    public async Task<ShiftAttendanceSummaryResult?> GetShiftSummaryAsync(
        string? shiftId = null,
        DateOnly? attendanceDate = null,
        bool includeDepartments = true,
        string? departmentId = null,
        CancellationToken cancellationToken = default)
    {
        var date = attendanceDate ?? DateOnly.FromDateTime(DateTime.Now);

        // Resolve shift
        string source = "USER_SELECTED";
        var resolvedShift = (await _context.MstShifts.ToListAsync(cancellationToken))
            .FirstOrDefault(s => !string.IsNullOrWhiteSpace(shiftId) && s.Id == shiftId);

        if (resolvedShift == null)
        {
            if (!string.IsNullOrWhiteSpace(shiftId))
            {
                // requested shift not found
                return null;
            }

            // Auto resolve based on current server time
            source = "AUTO";
            var now = TimeOnly.FromDateTime(DateTime.Now);
            var shifts = await _context.MstShifts.Where(s => s.ShiftType == "Roster").ToListAsync(cancellationToken);
            resolvedShift = shifts.FirstOrDefault(s =>
                (s.StartTime <= s.EndTime && now >= s.StartTime && now < s.EndTime)
                || (s.StartTime > s.EndTime && (now >= s.StartTime || now < s.EndTime)));

            if (resolvedShift == null) return null; // unable to determine
        }

        // Query attendances for the resolved shift and date
        var baseQuery = _context.AttProcessedAttendances
            .Where(a => a.AttendanceDate == date && a.ShiftId == resolvedShift.Id);

        // Join with employee employment to resolve department membership
        var deptQuery = from a in baseQuery
                        join emp in _context.HrmEmployeeEmployments
                            on a.EmployeeId equals emp.EmployeeId into empj
                        from emp in empj.DefaultIfEmpty()
                        select new
                        {
                            DepartmentId = emp != null ? emp.DepartmentId ?? string.Empty : string.Empty,
                            a.EmployeeId,
                            a.AttendanceStatus,
                            a.OtPayableHours
                        };

        // Apply optional department filter
        if (!string.IsNullOrWhiteSpace(departmentId))
        {
            deptQuery = deptQuery.Where(d => d.DepartmentId == departmentId);
        }

        var deptGroups = await deptQuery
            .GroupBy(d => d.DepartmentId)
            .Select(g => new
            {
                DepartmentId = g.Key,
                TotalEmployees = g.Select(x => x.EmployeeId).Distinct().Count(),
                PresentCount = g.Count(x => x.AttendanceStatus == "P" || x.AttendanceStatus == "Present"),
                AbsentCount = g.Count(x => x.AttendanceStatus == "A" || x.AttendanceStatus == "Absent"),
                OnOtCount = g.Count(x => x.OtPayableHours > 0)
            })
            .ToListAsync(cancellationToken);

        // Resolve department names from master, if available
        var deptIds = deptGroups.Select(d => d.DepartmentId).Where(id => !string.IsNullOrEmpty(id)).Distinct().ToList();
        var deptNames = new Dictionary<string, string>();
        if (deptIds.Any())
        {
            var deptMasters = await _context.MstDepartments
                .Where(d => deptIds.Contains(d.Id))
                .ToListAsync(cancellationToken);

            deptNames = deptMasters.ToDictionary(d => d.Id, d => string.IsNullOrWhiteSpace(d.DepartmentName) ? d.Id : d.DepartmentName);
        }

        var departments = deptGroups.Select(g =>
        {
            var total = g.TotalEmployees;
            var present = g.PresentCount;
            var onot = g.OnOtCount;
            var totalOnDutyDept = present + onot;
            var presentPct = total > 0 ? Math.Round((decimal)present * 100m / total, 2) : 0m;

            var deptName = string.Empty;
            if (!string.IsNullOrEmpty(g.DepartmentId) && deptNames.TryGetValue(g.DepartmentId, out var name)) deptName = name;
            if (string.IsNullOrEmpty(deptName)) deptName = string.IsNullOrEmpty(g.DepartmentId) ? "Unknown" : g.DepartmentId;

            return new DepartmentAttendance(
                g.DepartmentId,
                deptName,
                total,
                present,
                g.AbsentCount,
                onot,
                totalOnDutyDept,
                presentPct);
        }).ToList();

        // Aggregate totals from departments to ensure consistency
        var totalEmployees = departments.Sum(d => d.TotalEmployees);
        var presentCount = departments.Sum(d => d.PresentCount);
        var absentCount = departments.Sum(d => d.AbsentCount);
        var onOtCount = departments.Sum(d => d.OnOtCount);
        var totalOnDuty = departments.Sum(d => d.TotalOnDuty);

        decimal presentPercentage = totalEmployees > 0 ? Math.Round((decimal)presentCount * 100m / totalEmployees, 2) : 0m;
        decimal absentPercentage = totalEmployees > 0 ? Math.Round((decimal)absentCount * 100m / totalEmployees, 2) : 0m;
        decimal onOtPercentage = totalEmployees > 0 ? Math.Round((decimal)onOtCount * 100m / totalEmployees, 2) : 0m;
        decimal totalOnDutyPercentage = totalEmployees > 0 ? Math.Round((decimal)totalOnDuty * 100m / totalEmployees, 2) : 0m;

        var summary = new ShiftSummary(
            totalEmployees,
            presentCount,
            presentPercentage,
            absentCount,
            absentPercentage,
            onOtCount,
            onOtPercentage,
            totalOnDuty,
            totalOnDutyPercentage);

        var totals = new ShiftTotals(
            totalEmployees,
            presentCount,
            absentCount,
            onOtCount,
            totalOnDuty,
            presentPercentage);

        var result = new ShiftAttendanceSummaryResult(
            new ShiftInfo(resolvedShift.Id, resolvedShift.ShiftName, source),
            date,
            DateTime.UtcNow,
            summary,
            departments,
            totals);

        return result;
    }
}
