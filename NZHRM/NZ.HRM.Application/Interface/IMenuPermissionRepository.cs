using NZ.HRM.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMenuPermissionRepository
{
    Task<MenuPermission?> FindByIdAsync(string id);
    Task AddAsync(MenuPermission menuPermission);
    Task RemoveAsync(MenuPermission menuPermission);
    Task UpdateAsync(MenuPermission menuPermission);
    Task<List<MenuPermission>> GetAllAsync();
    Task SaveChangesAsync();
}
