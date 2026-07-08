using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IGroupComplexRepository
{
    Task<List<MstGroupComplex>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<MstGroupComplex?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(MstGroupComplex groupComplex, CancellationToken cancellationToken = default);
    Task UpdateAsync(MstGroupComplex groupComplex, CancellationToken cancellationToken = default);
    Task DeleteAsync(MstGroupComplex groupComplex, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
