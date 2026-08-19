namespace NZ.Payroll.Domain.Contracts;

/// <summary>
/// Represents a payroll processing request for a set of employees within a period.
/// </summary>
public record PayrollProcessRequest(
    string PayrollGroupId,
    int Year,
    int Month,
    IReadOnlyList<string> EmployeeIds,
    string ProcessedBy);

/// <summary>
/// Represents the result of processing payroll for one employee.
/// </summary>
public record EmployeePayrollResult(
    string EmployeeId,
    string EmployeeCode,
    decimal GrossSalary,
    decimal TotalDeductions,
    decimal NetSalary,
    bool IsSuccess,
    string? ErrorMessage = null);
