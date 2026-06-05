using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface ICompanyRepository
{
    Task<List<MstUnit>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<MstUnit?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(MstUnit unit, CancellationToken cancellationToken = default);
    Task UpdateAsync(MstUnit unit, CancellationToken cancellationToken = default);
    Task DeleteAsync(MstUnit unit, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}