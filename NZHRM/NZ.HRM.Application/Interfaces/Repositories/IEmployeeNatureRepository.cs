using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IEmployeeNatureRepository
{
    Task<List<EmployeeNature>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<EmployeeNature?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(EmployeeNature employeeNature, CancellationToken cancellationToken = default);
    Task UpdateAsync(EmployeeNature employeeNature, CancellationToken cancellationToken = default);
    Task DeleteAsync(EmployeeNature employeeNature, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
