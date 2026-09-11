namespace NZ.HRM.Application.EmployeeTransfers.Queries.GetEmployeeTransferById;

public class EmployeeTransferDetailDto
{
	public string Id { get; set; } = string.Empty;
	public string EmployeeId { get; set; } = string.Empty;
	public string EmployeeCode { get; set; } = string.Empty;
	public string EmployeeName { get; set; } = string.Empty;
	public string PreviousDepartmentId { get; set; } = string.Empty;
	public string PreviousDepartmentName { get; set; } = string.Empty;
	public string PreviousSectionId { get; set; } = string.Empty;
	public string PreviousSectionName { get; set; } = string.Empty;
	public string NewDepartmentId { get; set; } = string.Empty;
	public string NewDepartmentName { get; set; } = string.Empty;
	public string NewSectionId { get; set; } = string.Empty;
	public string NewSectionName { get; set; } = string.Empty;
	public DateOnly EffectiveFrom { get; set; }
	public string? Remarks { get; set; }
	public DateTime CreatedOn { get; set; }
	public string CreatedBy { get; set; } = string.Empty;
	public DateTime UpdatedOn { get; set; }
	public string UpdatedBy { get; set; } = string.Empty;
	public bool IsActive { get; set; }
}
