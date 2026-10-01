namespace NZ.Payroll.Application.PromotionIncrementRequests.DTOs;

public class PromotionIncrementRequestListItemDto
{
	public string RequestId { get; set; } = string.Empty;
	public string EmployeeId { get; set; } = string.Empty;
	public string EmployeeCode { get; set; } = string.Empty;
	public string EmployeeName { get; set; } = string.Empty;
	public string? Department { get; set; }
	public string? Section { get; set; }
	public string? CurrentDesignation { get; set; }
	public string? ProposedDesignation { get; set; }
	public string? CurrentGrade { get; set; }
	public string? CurrentGradeCode { get; set; }
	public string? ProposedGrade { get; set; }
	public string? ProposedGradeCode { get; set; }
	public DateOnly? LastIncrementDate { get; set; }
	public decimal? LastIncrementAmount { get; set; }
	public decimal CurrentGrossSalary { get; set; }
	public decimal IncrementPercent { get; set; }
	public decimal IncrementAmount { get; set; }
	public decimal NewGrossSalary { get; set; }
	public DateOnly EffectiveFrom { get; set; }
	public string? Reason { get; set; }
	public DateTime? DirectorApprovalDate { get; set; }
	public DateTime? MovementCellReviewDate { get; set; }
	public string Status { get; set; } = string.Empty;
}
