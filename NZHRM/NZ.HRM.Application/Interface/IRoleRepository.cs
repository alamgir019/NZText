// In Application or Domain Layer
using NZ.HRM.Domain.Entities;

public interface IRoleRepository
{
    Task<Role?> FindByIdAsync(string id);
    Task<List<Role>> GetAllAsync();
    Task AddAsync(Role role);
    Task RemoveAsync(Role role);
    Task UpdateAsync(Role role);
    Task SaveChangesAsync();
}
