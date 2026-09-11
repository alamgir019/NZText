namespace NZ.Attendance.Application.RawPunches.Queries.GetProcessedPunches;

public class GetProcessedPunchesQuery
{
    public string ShiftId { get; set; } = string.Empty;
    public string CompanyId { get; set; } = string.Empty;
    public string PunchType { get; set; } = string.Empty;
}