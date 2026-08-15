namespace NZ.Shared.Contracts.Attendance;

public interface IAttendanceSummaryQuery
{
    Task<AttendanceSummaryResult?> GetMonthlySummaryAsync(
        string employeeId,
        int year,
        int month,
        CancellationToken cancellationToken = default);
}

public record AttendanceSummaryResult(
    string EmployeeId,
    int PresentDays,
    int AbsentDays,
    int LateDays,
    decimal OvertimeHours,
    decimal LeaveDays);
