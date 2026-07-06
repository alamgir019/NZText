using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace NZ.HRM.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<SecUser?> FindByIdAsync(string id)
        {
            return await _db.SecUsers.FindAsync(id);
        }

        public async Task<List<SecUser>> GetAllAsync()
        {
            return await _db.SecUsers.ToListAsync();
        }

        public async Task AddAsync(SecUser user)
        {
            await _db.SecUsers.AddAsync(user);
        }

        public async Task RemoveAsync(SecUser user)
        {
            _db.SecUsers.Remove(user);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(SecUser user)
        {
            _db.SecUsers.Update(user);
            await Task.CompletedTask;
        }

        public async Task<SecUser?> FindByUsernameAsync(string username)
        {
            return await _db.SecUsers
                .Include(u => u.EmployeeMaster).ThenInclude(u => u.Employment)
                .Include(u => u.UserRoles).ThenInclude(u => u.Role)
                .ThenInclude(u => u.RolePermissions).ThenInclude(u => u.Permission)
                .FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}