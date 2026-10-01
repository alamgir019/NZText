using System.ComponentModel.DataAnnotations;

namespace NZ.Payroll.Application.PromotionIncrementRequests.Commands;

public class ForwardPromotionIncrementRequestsToCeoCommand
{
	[Required]
	[MinLength(1, ErrorMessage = "At least one promotion increment request must be selected")]
	public List<string> RequestIds { get; set; } = new();

	[MaxLength(500, ErrorMessage = "Remarks must not exceed 500 characters")]
	public string? Remarks { get; set; }
}
