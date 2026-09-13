using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.EmployeeShiftChanges.Commands.CreateEmployeeShiftChange;

public class CreateEmployeeShiftChangeCommand
{
	[Required(ErrorMessage = "Employee ID is required")]
	public string EmployeeId { get; set; } = string.Empty;

	[Required(ErrorMessage = "Previous shift ID is required")]
	public string PreviousShiftId { get; set; } = string.Empty;

	[Required(ErrorMessage = "New shift ID is required")]
	public string NewShiftId { get; set; } = string.Empty;

	[Required(ErrorMessage = "Effective date is required")]
	public DateOnly EffectiveFrom { get; set; }

	[MaxLength(500, ErrorMessage = "Remarks must not exceed 500 characters")]
	public string? Remarks { get; set; }
}
