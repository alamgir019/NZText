using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IShiftRepository
{
    Task<List<MstShift>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<MstShift?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(MstShift shift, CancellationToken cancellationToken = default);
    Task UpdateAsync(MstShift shift, CancellationToken cancellationToken = default);
    Task DeleteAsync(MstShift shift, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
