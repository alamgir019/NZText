namespace NZ.HRM.Application.EmployeeTransfers.Queries.GetAllEmployeeTransfers;

public class GetAllEmployeeTransfersQuery
{
	public bool IncludeInactive { get; set; } = false;

	public string? EmployeeId { get; set; }

	public DateOnly? FromDate { get; set; }

	public DateOnly? ToDate { get; set; }
}
