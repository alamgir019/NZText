namespace NZ.Payroll.Application.PayrollExceptions.DTOs;

public class PayrollExceptionRequestSummaryDto
{
    public int TotalRequests { get; set; }
    public int PendingWithMe { get; set; }
    public int ForwardedToHoIT { get; set; }
    public int Rejected { get; set; }
}
