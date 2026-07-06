using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IRolePermissionRepository
{
    Task<List<SecRolePermission>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<SecRolePermission?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(SecRolePermission secRolePermission, CancellationToken cancellationToken = default);
    Task UpdateAsync(SecRolePermission secRolePermission, CancellationToken cancellationToken = default);
    Task DeleteAsync(SecRolePermission secRolePermission, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
