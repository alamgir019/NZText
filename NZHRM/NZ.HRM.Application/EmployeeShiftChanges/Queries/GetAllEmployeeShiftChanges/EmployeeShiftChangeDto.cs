namespace NZ.HRM.Application.EmployeeShiftChanges.Queries.GetAllEmployeeShiftChanges;

public class EmployeeShiftChangeDto
{
	public string Id { get; set; } = string.Empty;
	public string EmployeeId { get; set; } = string.Empty;
	public string EmployeeName { get; set; } = string.Empty;
	public string PreviousShiftId { get; set; } = string.Empty;
	public string PreviousShiftName { get; set; } = string.Empty;
	public string NewShiftId { get; set; } = string.Empty;
	public string NewShiftName { get; set; } = string.Empty;
	public DateOnly EffectiveFrom { get; set; }
	public string? Remarks { get; set; }
	public bool IsActive { get; set; }
}
