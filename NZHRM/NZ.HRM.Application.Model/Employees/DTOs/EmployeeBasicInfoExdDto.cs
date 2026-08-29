namespace NZ.HRM.Application.Model.Employees.DTOs;

public class EmployeeBasicInfoExdDto
{
	public string Id { get; set; } = string.Empty;
	public string? EnrollmentId { get; set; }
	public string EmployeeNameEnglish { get; set; } = string.Empty;
	public string? MobileNumber { get; set; }
	public string? EmployeeNameBangla { get; set; }
	public string? DepartmentId { get; set; }
	public string? DepartmentName { get; set; }
	public string? DesignationId { get; set; }
	public string? EmployeeCode { get; set; }
	public string? DesignationName { get; set; }

	public string? SectionId { get; set; }
	public string? SectionName { get; set; }

	public string? GradeId { get; set; }
	public string? GradeName { get; set; }

	public string? ShiftId { get; set; }
	public string? ShiftName { get; set; }

	public DateOnly? DateOfJoining { get; set; }

	public decimal? BasicSalary { get; set; }
	public decimal? GrossSalary { get; set; }
	public string? EmployeeType { get; set; }

	public string? ForwardedBy { get; set; }

	public DateOnly? ForwardedDate { get; set; }

	public string? IncrementType { get; set; }
}
