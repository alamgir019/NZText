using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.EmployeeTransfers.Commands.UpdateEmployeeTransfer;

public class UpdateEmployeeTransferCommand
{
	[Required]
	public string Id { get; set; } = string.Empty;

	[Required(ErrorMessage = "Employee ID is required")]
	public string EmployeeId { get; set; } = string.Empty;

	[Required(ErrorMessage = "Previous department ID is required")]
	public string PreviousDepartmentId { get; set; } = string.Empty;

	[Required(ErrorMessage = "Previous section ID is required")]
	public string PreviousSectionId { get; set; } = string.Empty;

	[Required(ErrorMessage = "New department ID is required")]
	public string NewDepartmentId { get; set; } = string.Empty;

	[Required(ErrorMessage = "New section ID is required")]
	public string NewSectionId { get; set; } = string.Empty;

	[Required(ErrorMessage = "Effective date is required")]
	public DateOnly EffectiveFrom { get; set; }

	[MaxLength(500, ErrorMessage = "Remarks must not exceed 500 characters")]
	public string? Remarks { get; set; }
}
