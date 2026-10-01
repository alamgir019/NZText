namespace NZ.Payroll.Application.PromotionIncrementRequests.DTOs;

public class ForwardPromotionIncrementRequestsResultDto
{
	public int ForwardedCount { get; set; }
	public DateTime ForwardedOn { get; set; }
	public List<ForwardedPromotionIncrementRequestDto> Items { get; set; } = new();
}

public class ForwardedPromotionIncrementRequestDto
{
	public string RequestId { get; set; } = string.Empty;
	public string EmployeeId { get; set; } = string.Empty;
	public decimal CurrentGrossSalary { get; set; }
	public decimal NewGrossSalary { get; set; }
	public string Status { get; set; } = string.Empty;
}
