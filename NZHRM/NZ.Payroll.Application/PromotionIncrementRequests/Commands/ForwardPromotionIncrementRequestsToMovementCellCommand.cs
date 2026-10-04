using System.ComponentModel.DataAnnotations;

namespace NZ.Payroll.Application.PromotionIncrementRequests.Commands;

public class ForwardPromotionIncrementRequestsToMovementCellCommand
{
	[Required]
	[MinLength(1, ErrorMessage = "At least one promotion increment request must be selected")]
	public List<PromotionIncrementRequestActionDto> Requests { get; set; } = new();
}

public class PromotionIncrementRequestActionDto
{
	[Required(ErrorMessage = "Request ID is required")]
	public string RequestId { get; set; } = string.Empty;

	public bool Approved { get; set; } = true;

	[MaxLength(500, ErrorMessage = "Remarks must not exceed 500 characters")]
	public string? Remarks { get; set; }
}
