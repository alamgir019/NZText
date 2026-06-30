using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IEmployeeSalaryAccountRepository
{
    Task<List<HrmEmployeeSalaryAccount>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<HrmEmployeeSalaryAccount?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<HrmEmployeeSalaryAccount?> GetByEmployeeIdAsync(string employeeId, bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<string> AddAsync(HrmEmployeeSalaryAccount salaryAccount, CancellationToken cancellationToken = default);
    Task UpdateAsync(HrmEmployeeSalaryAccount salaryAccount, CancellationToken cancellationToken = default);
    Task DeleteAsync(HrmEmployeeSalaryAccount salaryAccount, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}