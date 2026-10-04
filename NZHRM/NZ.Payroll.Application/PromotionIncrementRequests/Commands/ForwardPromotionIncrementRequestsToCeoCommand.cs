using System.ComponentModel.DataAnnotations;

namespace NZ.Payroll.Application.PromotionIncrementRequests.Commands;

public class ForwardPromotionIncrementRequestsToCeoCommand
{
	[Required]
	[MinLength(1, ErrorMessage = "At least one promotion increment request must be selected")]
	public List<PromotionIncrementRequestActionDto> Requests { get; set; } = new();
}
