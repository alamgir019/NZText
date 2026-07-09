using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IBankRepository
{
    Task<List<LookBanking>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<LookBanking?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(LookBanking bank, CancellationToken cancellationToken = default);
    Task UpdateAsync(LookBanking bank, CancellationToken cancellationToken = default);
    Task DeleteAsync(LookBanking bank, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
