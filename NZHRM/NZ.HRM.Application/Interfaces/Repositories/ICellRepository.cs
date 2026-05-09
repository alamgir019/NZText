using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface ICellRepository
{
    Task<List<Cell>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<Cell?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<List<Cell>> GetBySectionIdAsync(string sectionId, CancellationToken cancellationToken = default);
    Task<string> AddAsync(Cell cell, CancellationToken cancellationToken = default);
    Task UpdateAsync(Cell cell, CancellationToken cancellationToken = default);
    Task DeleteAsync(Cell cell, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
