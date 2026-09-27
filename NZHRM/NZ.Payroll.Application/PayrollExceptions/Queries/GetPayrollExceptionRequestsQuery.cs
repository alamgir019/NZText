namespace NZ.Payroll.Application.PayrollExceptions.Queries;

public class GetPayrollExceptionRequestsQuery
{
    public string? RequestId { get; set; }
    public string? EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public string? Department { get; set; }
    public string? AdjustmentType { get; set; }
    public string? Status { get; set; }
    public DateOnly? AttendanceDateFrom { get; set; }
    public DateOnly? AttendanceDateTo { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
