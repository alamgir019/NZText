namespace NZ.Payroll.Application.PayrollExceptions.Commands;

public class ForwardPayrollExceptionRequestsResult
{
    public bool Success { get; set; }
    public string? ErrorCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public int ForwardedCount { get; set; }
    public DateTime? ForwardedOn { get; set; }
}
