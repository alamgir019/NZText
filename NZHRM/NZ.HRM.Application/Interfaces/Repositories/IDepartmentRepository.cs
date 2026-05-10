using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IDepartmentRepository
{
    Task<List<Department>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<Department?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(Department department, CancellationToken cancellationToken = default);
    Task UpdateAsync(Department department, CancellationToken cancellationToken = default);
    Task DeleteAsync(Department department, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
