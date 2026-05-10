using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IEmployeePersonalRepository
{
    Task<List<EmployeePersonal>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<EmployeePersonal?> GetByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken = default);
    Task<EmployeePersonal?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(EmployeePersonal employeePersonal, CancellationToken cancellationToken = default);
    Task UpdateAsync(EmployeePersonal employeePersonal, CancellationToken cancellationToken = default);
    Task DeleteAsync(EmployeePersonal employeePersonal, CancellationToken cancellationToken = default);
    Task<bool> ExistsForEmployeeAsync(string employeeId, CancellationToken cancellationToken = default);
}
