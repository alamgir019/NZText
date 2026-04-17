using Microsoft.EntityFrameworkCore;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

public class MenuPermissionRepository : IMenuPermissionRepository
{
    private readonly ApplicationDbContext _db;
    public MenuPermissionRepository(ApplicationDbContext db) => _db = db;

    public async Task<MenuPermission?> FindByIdAsync(string id) => await _db.MenuPermissions.FindAsync(id);

    public async Task AddAsync(MenuPermission menuPermission) => await _db.MenuPermissions.AddAsync(menuPermission);

    public async Task RemoveAsync(MenuPermission menuPermission)
    {
        _db.MenuPermissions.Remove(menuPermission);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(MenuPermission menuPermission)
    {
        _db.MenuPermissions.Update(menuPermission);
        await Task.CompletedTask;
    }

    public async Task<List<MenuPermission>> GetAllAsync() => await _db.MenuPermissions.ToListAsync();

    public async Task SaveChangesAsync() => await _db.SaveChangesAsync();

    public async Task<List<MenuPermission>> GetByUserIdAsync(string userId)
    {
        return await _db.MenuPermissions
            .Include(m => m.Menu)
            .Where(mp => mp.UserId == userId && mp.Visibility)
            .ToListAsync();
    }

    public async Task<List<MenuPermission>> GetByRoleIdAsync(string roleId)
    {
        return await _db.MenuPermissions
            .Where(mp => mp.RoleId == roleId && mp.UserId == null && mp.Visibility)
            .ToListAsync();
    }

}
