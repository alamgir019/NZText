namespace NZ.Attendance.Application.RawPunches.Queries.GetProcessedPunches;

public class ProcessedPunchListItemDto
{
    public string EmployeeId { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
    public string? EmployeePhotoUrl { get; set; }
    public DateOnly PunchDate { get; set; }
    public TimeOnly AdjustedPunchTime { get; set; }
    public TimeOnly RawPunchTime { get; set; }
    public string PunchType { get; set; } = string.Empty;
}