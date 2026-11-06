using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.EmployeeShiftChanges.Commands.UpdateEmployeeShiftChange;

public class UpdateEmployeeShiftChangeCommand
{
	[Required]
	public string Id { get; set; } = string.Empty;

	[Required(ErrorMessage = "Employee ID is required")]
	public string EmployeeId { get; set; } = string.Empty;

	public string? PreviousShiftId { get; set; }

	[Required(ErrorMessage = "New shift ID is required")]
	public string NewShiftId { get; set; } = string.Empty;

	[Required(ErrorMessage = "Effective date is required")]
	public DateOnly EffectiveFrom { get; set; }

	[Required(ErrorMessage = "Effective to date is required")]
	public DateOnly EffectiveTo { get; set; }

	[MaxLength(500, ErrorMessage = "Remarks must not exceed 500 characters")]
	public string? Remarks { get; set; }
}
