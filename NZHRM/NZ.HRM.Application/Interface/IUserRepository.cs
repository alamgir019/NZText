using NZ.HRM.Domain.Entities;

public interface IUserRepository
{
    Task<User?> FindByIdAsync(string id);
    Task<User?> FindByUsernameAsync(string username);
    Task<List<User>> GetAllAsync();
    Task AddAsync(User user);
    Task RemoveAsync(User user);
    Task UpdateAsync(User user);
    Task SaveChangesAsync();
}
