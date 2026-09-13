namespace NZ.HRM.Application.EmployeeShiftChanges.Queries.GetEmployeeShiftChangeById;

public class EmployeeShiftChangeDetailDto
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
	public DateTime CreatedOn { get; set; }
	public string CreatedBy { get; set; } = string.Empty;
	public DateTime UpdatedOn { get; set; }
	public string UpdatedBy { get; set; } = string.Empty;
	public bool IsActive { get; set; }
}
