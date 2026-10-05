using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NZ.Payroll.Application.PromotionIncrementRequests.Commands;

public class PromotionIncrementRequestItem
{
	public string EmployeeId { get; set; } = string.Empty;
	public string? EmployeeName { get; set; }
	public string? Department { get; set; }
	public string? CurrentDesignation { get; set; }
	public string? ProposedDesignation { get; set; }
	public string? CurrentGrade { get; set; }
	public string? NewGrade { get; set; }

	[Required(ErrorMessage = "Proposed designation is required")]
	public string ProposedDesignationId { get; set; } = string.Empty;

	[Required(ErrorMessage = "New grade is required")]
	public string ProposedGradeId { get; set; } = string.Empty;

	[Range(0.01, 100, ErrorMessage = "Increment percent must be greater than 0 and at most 100")]
	public decimal IncrementPercent { get; set; }

	[Range(0.01, double.MaxValue, ErrorMessage = "Increment amount must be greater than 0")]
	public decimal IncrementAmount { get; set; }

	[Required(ErrorMessage = "Effective date is required")]
	public DateOnly EffectiveFrom { get; set; }

	[MaxLength(500, ErrorMessage = "Reason must not exceed 500 characters")]
	public string? Reason { get; set; }
}

public class ForwardPromotionIncrementRequestsToDirectorCommand
{
	[Required]
	[MinLength(1, ErrorMessage = "At least one employee must be selected for forwarding.")]
	public List<PromotionIncrementRequestItem> Requests { get; set; } = new();

	[MaxLength(500, ErrorMessage = "Remarks must not exceed 500 characters")]
	public string? Remarks { get; set; }
}
