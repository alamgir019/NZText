using NZ.HRM.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMenuPermissionRepository
{
    Task<SecPermission?> FindByIdAsync(string id);
    Task AddAsync(SecPermission secPermission);
    Task RemoveAsync(SecPermission secPermission);
    Task UpdateAsync(SecPermission secPermission);
    Task<List<SecPermission>> GetAllAsync();
    Task SaveChangesAsync();
    Task<List<SecPermission>> GetByUserIdAsync(string userId);
    Task<List<SecPermission>> GetByRoleIdAsync(string roleId);

}
