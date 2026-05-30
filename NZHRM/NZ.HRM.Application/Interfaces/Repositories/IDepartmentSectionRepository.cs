using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IDepartmentSectionRepository
{
    Task<List<DepartmentSection>> GetAllAsync(bool includeInactive = false, string? departmentId = null, string? sectionId = null, CancellationToken cancellationToken = default);
    Task<DepartmentSection?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string?> GetDepartmentIdBySectionIdAsync(string sectionId, CancellationToken cancellationToken = default);
    Task<string?> GetDepartmentNameBySectionIdAsync(string sectionId, CancellationToken cancellationToken = default);
    Task<string> AddAsync(DepartmentSection departmentSection, CancellationToken cancellationToken = default);
    Task UpdateAsync(DepartmentSection departmentSection, CancellationToken cancellationToken = default);
    Task DeleteAsync(DepartmentSection departmentSection, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
    Task SetDepartmentForSectionAsync(string sectionId, string departmentId, CancellationToken cancellationToken = default);
}
