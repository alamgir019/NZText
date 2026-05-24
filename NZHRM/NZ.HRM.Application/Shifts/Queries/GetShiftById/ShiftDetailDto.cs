namespace NZ.HRM.Application.Shifts.Queries.GetShiftById;

public class ShiftDetailDto
{
    public string Id { get; set; } = string.Empty;
    public string ShiftName { get; set; } = string.Empty;
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public int SortOrder { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime UpdatedOn { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
