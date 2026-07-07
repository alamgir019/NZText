using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IDivisionRepository
{
    Task<List<LookDivision>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<LookDivision?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(LookDivision division, CancellationToken cancellationToken = default);
    Task UpdateAsync(LookDivision division, CancellationToken cancellationToken = default);
    Task DeleteAsync(LookDivision division, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
