using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface ICellRepository
{
    Task<List<MstCell>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<MstCell?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<List<MstCell>> GetBySectionIdAsync(string sectionId, bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<string> AddAsync(MstCell cell, CancellationToken cancellationToken = default);
    Task UpdateAsync(MstCell cell, CancellationToken cancellationToken = default);
    Task DeleteAsync(MstCell cell, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
