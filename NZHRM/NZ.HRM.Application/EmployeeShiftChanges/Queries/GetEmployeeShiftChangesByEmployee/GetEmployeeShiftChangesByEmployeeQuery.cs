namespace NZ.HRM.Application.EmployeeShiftChanges.Queries.GetEmployeeShiftChangesByEmployee;

public class GetEmployeeShiftChangesByEmployeeQuery
{
	public string EmployeeId { get; set; } = string.Empty;

	public bool IncludeInactive { get; set; } = false;

	public DateOnly? FromDate { get; set; }

	public DateOnly? ToDate { get; set; }
}
