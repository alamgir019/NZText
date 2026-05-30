using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IGradeRepository
{
    Task<List<Grade>> GetAllAsync(bool includeInactive = false, string? employeeType = null, CancellationToken cancellationToken = default);
    Task<Grade?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(Grade grade, CancellationToken cancellationToken = default);
    Task UpdateAsync(Grade grade, CancellationToken cancellationToken = default);
    Task DeleteAsync(Grade grade, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
