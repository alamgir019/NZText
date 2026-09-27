namespace NZ.Payroll.Application.PayrollExceptions.DTOs;

public class PayrollExceptionRequestDto
{
    public string RequestId { get; set; } = string.Empty;
    public DateOnly? AttendanceDate { get; set; }
    public PayrollExceptionRequestEmployeeDto Employee { get; set; } = new();
    public string? AdjustmentType { get; set; }
    public string? Shift { get; set; }
    public string? ShiftTime { get; set; }
    public decimal? ImpactAmount { get; set; }
    public string? SubmittedBy { get; set; }
    public DateTime SubmittedOn { get; set; }
    public string? Status { get; set; }
}
