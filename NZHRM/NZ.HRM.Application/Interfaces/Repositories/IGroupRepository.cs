using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IGroupRepository
{
    Task<List<MstGroup>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<MstGroup?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(MstGroup group, CancellationToken cancellationToken = default);
    Task UpdateAsync(MstGroup group, CancellationToken cancellationToken = default);
    Task DeleteAsync(MstGroup group, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
