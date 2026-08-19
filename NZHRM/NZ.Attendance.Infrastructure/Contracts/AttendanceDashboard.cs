using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NZ.Attendance.Infrastructure.Contracts;

public interface IAttendanceDashboardQuery
{
    Task<ShiftAttendanceSummaryResult?> GetShiftSummaryAsync(
        string? shiftId = null,
        DateOnly? attendanceDate = null,
        bool includeDepartments = true,
        string? departmentId = null,
        CancellationToken cancellationToken = default);
}

public record ShiftAttendanceSummaryResult(
    ShiftInfo Shift,
    DateOnly AttendanceDate,
    DateTime GeneratedTimestamp,
    ShiftSummary Summary,
    IEnumerable<DepartmentAttendance> Departments,
    ShiftTotals Totals);

public record ShiftInfo(string ShiftId, string ShiftName, string Source);

public record ShiftSummary(
    int TotalEmployees,
    int PresentCount,
    decimal PresentPercentage,
    int AbsentCount,
    decimal AbsentPercentage,
    int OnOtCount,
    decimal OnOtPercentage,
    int TotalOnDuty,
    decimal TotalOnDutyPercentage);

public record DepartmentAttendance(
    string DepartmentId,
    string DepartmentName,
    int TotalEmployees,
    int PresentCount,
    int AbsentCount,
    int OnOtCount,
    int TotalOnDuty,
    decimal PresentPercentage);

public record ShiftTotals(
    int TotalEmployees,
    int PresentCount,
    int AbsentCount,
    int OnOtCount,
    int TotalOnDuty,
    decimal PresentPercentage);
