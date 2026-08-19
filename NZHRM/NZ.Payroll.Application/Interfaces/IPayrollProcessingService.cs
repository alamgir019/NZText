using NZ.Payroll.Domain.Contracts;

namespace NZ.Payroll.Application.Interfaces;

/// <summary>
/// Orchestrates the payroll processing for a group of employees.
/// </summary>
public interface IPayrollProcessingService
{
    Task<IReadOnlyList<EmployeePayrollResult>> ProcessAsync(
        PayrollProcessRequest request,
        CancellationToken cancellationToken = default);
}
