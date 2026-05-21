using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface ISectionRepository
{
    Task<List<Section>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<List<Section>> GetByDepartmentIdAsync(string departmentId, bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<Section?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(Section section, CancellationToken cancellationToken = default);
    Task UpdateAsync(Section section, CancellationToken cancellationToken = default);
    Task DeleteAsync(Section section, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
