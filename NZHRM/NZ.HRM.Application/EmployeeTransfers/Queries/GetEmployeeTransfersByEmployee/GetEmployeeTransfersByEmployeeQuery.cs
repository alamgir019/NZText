namespace NZ.HRM.Application.EmployeeTransfers.Queries.GetEmployeeTransfersByEmployee;

public class GetEmployeeTransfersByEmployeeQuery
{
	public string EmployeeId { get; set; } = string.Empty;

	public bool IncludeInactive { get; set; } = false;

	public DateOnly? FromDate { get; set; }

	public DateOnly? ToDate { get; set; }
}
