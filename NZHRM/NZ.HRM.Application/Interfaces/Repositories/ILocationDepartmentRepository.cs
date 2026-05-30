using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface ILocationDepartmentRepository
{
    Task<List<LocationDepartment>> GetAllAsync(bool includeInactive = false, string? locationId = null, string? departmentId = null, CancellationToken cancellationToken = default);
    Task<LocationDepartment?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string?> GetLocationIdByDepartmentIdAsync(string departmentId, CancellationToken cancellationToken = default);
    Task<string?> GetLocationNameByDepartmentIdAsync(string departmentId, CancellationToken cancellationToken = default);
    Task SetLocationForDepartmentAsync(string departmentId, string locationId, CancellationToken cancellationToken = default);
    Task<string> AddAsync(LocationDepartment locationDepartment, CancellationToken cancellationToken = default);
    Task UpdateAsync(LocationDepartment locationDepartment, CancellationToken cancellationToken = default);
    Task DeleteAsync(LocationDepartment locationDepartment, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
