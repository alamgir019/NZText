using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IComplexUnitDepartmentRepository
{
    Task<List<MstDepartmentUnitComplex>> GetAllAsync(bool includeInactive = false, string? complexId = null, string? departmentId = null, CancellationToken cancellationToken = default);
    Task<MstDepartmentUnitComplex?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string?> GetComplexIdByDepartmentIdAsync(string departmentId, CancellationToken cancellationToken = default);
    Task<string?> GetComplexNameByDepartmentIdAsync(string departmentId, CancellationToken cancellationToken = default);
    Task SetComplexForDepartmentAsync(string departmentId, string complexId, CancellationToken cancellationToken = default);
    Task<string> AddAsync(MstDepartmentUnitComplex locationDepartment, CancellationToken cancellationToken = default);
    Task UpdateAsync(MstDepartmentUnitComplex locationDepartment, CancellationToken cancellationToken = default);
    Task DeleteAsync(MstDepartmentUnitComplex locationDepartment, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
