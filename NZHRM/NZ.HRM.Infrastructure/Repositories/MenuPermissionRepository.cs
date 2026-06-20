using Microsoft.EntityFrameworkCore;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories
{
    public class MenuPermissionRepository : IMenuPermissionRepository
    {
        private readonly ApplicationDbContext _db;
        public MenuPermissionRepository(ApplicationDbContext db) => _db = db;

        public async Task<SecPermission?> FindByIdAsync(string id) => await _db.SecPermissions.FindAsync(id);

        public async Task AddAsync(SecPermission secPermission) => await _db.SecPermissions.AddAsync(secPermission);

        public async Task RemoveAsync(SecPermission secPermission)
        {
            _db.SecPermissions.Remove(secPermission);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(SecPermission secPermission)
        {
            _db.SecPermissions.Update(secPermission);
            await Task.CompletedTask;
        }

        public async Task<List<SecPermission>> GetAllAsync() => await _db.SecPermissions.ToListAsync();

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();

        public async Task<List<SecPermission>> GetByUserIdAsync(string userId)
        {
            return await _db.SecPermissions
                //.Include(m => m.sec)
                //.Where(mp => mp.UserId == userId && mp.Visibility)
                .ToListAsync();
        }

        public async Task<List<SecPermission>> GetByRoleIdAsync(string roleId)
        {
            return await _db.SecPermissions
                //.Where(mp => mp.RoleId == roleId && mp.UserId == null && mp.Visibility)
                .ToListAsync();
        }

    }
}