// In Application or Domain Layer
using NZ.HRM.Domain.Entities;

public interface IRoleRepository
{
    Task<SecRole?> FindByIdAsync(string id);
    Task<List<SecRole>> GetAllAsync();
    Task AddAsync(SecRole role);
    Task RemoveAsync(SecRole role);
    Task UpdateAsync(SecRole role);
    Task SaveChangesAsync();
}
