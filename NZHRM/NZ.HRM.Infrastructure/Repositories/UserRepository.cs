using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<User?> FindByIdAsync(string id)
    {
        return await _db.Users.FindAsync(id);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _db.Users.ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        await _db.Users.AddAsync(user);
    }

    public async Task RemoveAsync(User user)
    {
        _db.Users.Remove(user);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(User user)
    {
        _db.Users.Update(user);
        await Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}
