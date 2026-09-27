namespace NZ.Payroll.Application.PayrollExceptions.DTOs;

public class PayrollExceptionRequestsResponseDto
{
    public PayrollExceptionRequestSummaryDto Summary { get; set; } = new();
    public PayrollExceptionPaginationDto Pagination { get; set; } = new();
    public List<PayrollExceptionRequestDto> Items { get; set; } = new();
}
