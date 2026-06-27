using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IEmployeeNatureRepository
{
    Task<List<LookEmployeeNature>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<LookEmployeeNature?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(LookEmployeeNature employeeNature, CancellationToken cancellationToken = default);
    Task UpdateAsync(LookEmployeeNature employeeNature, CancellationToken cancellationToken = default);
    Task DeleteAsync(LookEmployeeNature employeeNature, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
