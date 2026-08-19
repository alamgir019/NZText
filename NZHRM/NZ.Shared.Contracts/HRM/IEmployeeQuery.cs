namespace NZ.Shared.Contracts.HRM;

public interface IEmployeeQuery
{
    Task<EmployeeInfo?> GetByIdAsync(string employeeId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EmployeeInfo>> GetActiveByUnitAsync(string unitCode, CancellationToken cancellationToken = default);
}

public record EmployeeInfo(
    string EmployeeId,
    string EmployeeCode,
    string FullName,
    string Designation,
    string Grade,
    string Department,
    string PayrollGroup);
