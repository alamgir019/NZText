// In Infrastructure Layer
using Microsoft.EntityFrameworkCore;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _db;
    public RoleRepository(ApplicationDbContext db) => _db = db;

    public async Task<Role?> FindByIdAsync(string id) => await _db.Roles.FindAsync(id);
    public async Task AddAsync(Role role) => await _db.Roles.AddAsync(role);
    public async Task RemoveAsync(Role role)
    {
        _db.Roles.Remove(role);
        await Task.CompletedTask; // Ensures the method remains asynchronous
    }

    public async Task UpdateAsync(Role role)
    {
        _db.Roles.Update(role);
        await Task.CompletedTask;
    }

    public async Task<List<Role>> GetAllAsync() => await _db.Roles.ToListAsync();

    public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
}
