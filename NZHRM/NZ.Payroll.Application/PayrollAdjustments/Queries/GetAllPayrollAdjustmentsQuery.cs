namespace NZ.Payroll.Application.PayrollAdjustments.Queries;

public class GetAllPayrollAdjustmentsQuery
{
    public string? AttendanceMonth { get; set; }
    public string? CompanyId { get; set; }
    public string? EmployeeId { get; set; }
    public string? Status { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
