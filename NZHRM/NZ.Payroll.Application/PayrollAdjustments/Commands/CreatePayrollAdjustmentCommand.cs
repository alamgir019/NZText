using System.ComponentModel.DataAnnotations;

namespace NZ.Payroll.Application.PayrollAdjustments.Commands;

public class CreatePayrollAdjustmentCommand
{
    [Required]
    [MaxLength(50)]
    public string EmployeeId { get; set; } = string.Empty;

    [Required]
    [MaxLength(7)]
    public string AttendanceMonth { get; set; } = string.Empty; // e.g., 2025-04

    [Required]
    public string CorrectionType { get; set; } = string.Empty;

    [Required]
    public string Reason { get; set; } = string.Empty;

    public string? SupportingDocumentId { get; set; }
    public string? Remarks { get; set; }
}
