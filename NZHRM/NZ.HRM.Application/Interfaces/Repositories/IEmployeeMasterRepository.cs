using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IEmployeeMasterRepository
{
    Task<List<EmployeeMaster>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<EmployeeMaster?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<EmployeeMaster?> GetByEmployeeCodeAsync(string employeeCode, CancellationToken cancellationToken = default);
    Task<List<EmployeeMaster>> GetByCompanyIdAsync(string companyId, CancellationToken cancellationToken = default);
    Task<List<EmployeeMaster>> GetByDepartmentIdAsync(string departmentId, CancellationToken cancellationToken = default);
    Task<string> AddAsync(EmployeeMaster employeeMaster, CancellationToken cancellationToken = default);
    Task UpdateAsync(EmployeeMaster employeeMaster, CancellationToken cancellationToken = default);
    Task DeleteAsync(EmployeeMaster employeeMaster, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
    Task<bool> EmployeeCodeExistsAsync(string employeeCode, CancellationToken cancellationToken = default);
}
