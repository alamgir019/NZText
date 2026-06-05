using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IDepartmentRepository
{
    Task<List<MstDepartment>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<MstDepartment?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(MstDepartment department, CancellationToken cancellationToken = default);
    Task UpdateAsync(MstDepartment department, CancellationToken cancellationToken = default);
    Task DeleteAsync(MstDepartment department, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
