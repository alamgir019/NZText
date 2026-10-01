namespace NZ.Payroll.Application.PromotionIncrementRequests.DTOs;

public class PromotionIncrementEmployeeSnapshot
{
	public string EmployeeId { get; set; } = string.Empty;
	public string? DepartmentId { get; set; }
	public string? SectionId { get; set; }
	public string? DesignationId { get; set; }
	public string? GradeId { get; set; }
	public decimal? GrossSalary { get; set; }
}
