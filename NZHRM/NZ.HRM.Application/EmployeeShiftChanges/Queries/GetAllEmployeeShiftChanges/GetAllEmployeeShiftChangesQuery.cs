namespace NZ.HRM.Application.EmployeeShiftChanges.Queries.GetAllEmployeeShiftChanges;

public class GetAllEmployeeShiftChangesQuery
{
	public bool IncludeInactive { get; set; } = false;

	public string? EmployeeId { get; set; }

	public DateOnly? FromDate { get; set; }

	public DateOnly? ToDate { get; set; }
}
