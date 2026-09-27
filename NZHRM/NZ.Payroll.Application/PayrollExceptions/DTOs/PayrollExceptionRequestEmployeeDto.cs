namespace NZ.Payroll.Application.PayrollExceptions.DTOs;

public class PayrollExceptionRequestEmployeeDto
{
    public string EmployeeId { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
}
