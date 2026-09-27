namespace NZ.Payroll.Application.PayrollExceptions.DTOs;

public class PayrollExceptionPaginationDto
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }
}
