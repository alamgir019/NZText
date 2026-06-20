using NZ.HRM.Domain.Entities;

public interface IUserRepository
{
    Task<SecUser?> FindByIdAsync(string id);
    Task<SecUser?> FindByUsernameAsync(string username);
    Task<List<SecUser>> GetAllAsync();
    Task AddAsync(SecUser user);
    Task RemoveAsync(SecUser user);
    Task UpdateAsync(SecUser user);
    Task SaveChangesAsync();
}
