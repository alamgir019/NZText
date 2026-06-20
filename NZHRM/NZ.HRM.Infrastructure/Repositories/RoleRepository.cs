// In Infrastructure Layer
using Microsoft.EntityFrameworkCore;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _db;
        public RoleRepository(ApplicationDbContext db) => _db = db;

        public async Task<SecRole?> FindByIdAsync(string id) => await _db.SecRoles.FindAsync(id);
        public async Task AddAsync(SecRole role) => await _db.SecRoles.AddAsync(role);
        public async Task RemoveAsync(SecRole role)
        {
            _db.SecRoles.Remove(role);
            await Task.CompletedTask; // Ensures the method remains asynchronous
        }

        public async Task UpdateAsync(SecRole role)
        {
            _db.SecRoles.Update(role);
            await Task.CompletedTask;
        }

        public async Task<List<SecRole>> GetAllAsync() => await _db.SecRoles.ToListAsync();

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}