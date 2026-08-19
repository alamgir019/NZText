using NZ.Payroll.Domain.Contracts;

namespace NZ.Payroll.Application.Interfaces;

/// <summary>
/// Core payroll calculation engine interface.
/// </summary>
public interface IPayrollCalculationService
{
    Task<EmployeePayrollResult> CalculateAsync(
        string employeeId,
        int year,
        int month,
        CancellationToken cancellationToken = default);
}
