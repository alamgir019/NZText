using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface ILocationDepartmentRepository
{
    Task<List<MstSubunitDepartment>> GetAllAsync(bool includeInactive = false, string? locationId = null, string? departmentId = null, CancellationToken cancellationToken = default);
    Task<MstSubunitDepartment?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string?> GetLocationIdByDepartmentIdAsync(string departmentId, CancellationToken cancellationToken = default);
    Task<string?> GetLocationNameByDepartmentIdAsync(string departmentId, CancellationToken cancellationToken = default);
    Task SetLocationForDepartmentAsync(string departmentId, string locationId, CancellationToken cancellationToken = default);
    Task<string> AddAsync(MstSubunitDepartment locationDepartment, CancellationToken cancellationToken = default);
    Task UpdateAsync(MstSubunitDepartment locationDepartment, CancellationToken cancellationToken = default);
    Task DeleteAsync(MstSubunitDepartment locationDepartment, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
