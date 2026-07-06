using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IUserRoleRepository
{
    Task<List<SecUserRole>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<SecUserRole?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(SecUserRole secUserRole, CancellationToken cancellationToken = default);
    Task UpdateAsync(SecUserRole secUserRole, CancellationToken cancellationToken = default);
    Task DeleteAsync(SecUserRole secUserRole, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
