using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface ISectionRepository
{
    Task<List<MstSection>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<List<MstSection>> GetByDepartmentIdAsync(string departmentId, bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<MstSection?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(MstSection section, CancellationToken cancellationToken = default);
    Task UpdateAsync(MstSection section, CancellationToken cancellationToken = default);
    Task DeleteAsync(MstSection section, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
