using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IShiftRepository
{
    Task<List<Shift>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<Shift?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(Shift shift, CancellationToken cancellationToken = default);
    Task UpdateAsync(Shift shift, CancellationToken cancellationToken = default);
    Task DeleteAsync(Shift shift, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
