using NZ.Payroll.Application.PayIncrementHistories.DTOs;
using System.ComponentModel.DataAnnotations;

namespace NZ.Payroll.Application.PayIncrementHistories.Commands;

public class CreatePayIncrementHistoryCommand
{
	[Required(ErrorMessage = "Employee ID is required")]
	[MaxLength(50, ErrorMessage = "Employee ID must not exceed 50 characters")]
	public string EmployeeId { get; set; } = string.Empty;

	[Required(ErrorMessage = "Effective date is required")]
	public DateOnly EffectiveDate { get; set; }

	[Range(0, double.MaxValue, ErrorMessage = "Old gross salary must be a positive value")]
	public decimal? OldGrossSalary { get; set; }

	[Range(0, double.MaxValue, ErrorMessage = "New gross salary must be a positive value")]
	public decimal? NewGrossSalary { get; set; }

	[Range(0, double.MaxValue, ErrorMessage = "Increment amount must be a positive value")]
	public decimal? IncrementAmount { get; set; }

	[Range(0, 100, ErrorMessage = "Increment percent must be between 0 and 100")]
	public decimal? IncrementPercent { get; set; }

	[MaxLength(100, ErrorMessage = "Approved by must not exceed 100 characters")]
	public string? ApprovedBy { get; set; }

	public DateTime? ApprovalDate { get; set; }

	[MaxLength(100, ErrorMessage = "Forwarded by must not exceed 100 characters")]
	public string? ForwardedBy { get; set; }

	public DateTime? ForwardDate { get; set; }

	[MaxLength(50, ErrorMessage = "Increment type must not exceed 50 characters")]
	public string? IncrementType { get; set; }
}
