using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IEmployeeMasterRepository
{
    // If onDate is provided, returns employees relevant to that date (e.g., created on that date). If null returns all.
    Task<List<EmployeeMaster>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<EmployeeMaster?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<EmployeeMaster?> GetByEmployeeCodeAsync(string employeeCode, CancellationToken cancellationToken = default);
    Task<List<EmployeeMaster>> GetByCompanyIdAsync(string companyId, CancellationToken cancellationToken = default);
    Task<List<EmployeeMaster>> GetByDepartmentIdAsync(string departmentId, CancellationToken cancellationToken = default);
    Task<List<EmployeeMaster>> SearchAsync(string searchText, CancellationToken cancellationToken = default);
    Task<List<EmployeeMaster>> GetByDateAsync(DateTime onDatel, bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<string> AddAsync(EmployeeMaster employeeMaster, CancellationToken cancellationToken = default);
    Task UpdateAsync(EmployeeMaster employeeMaster, CancellationToken cancellationToken = default);
    Task DeleteAsync(EmployeeMaster employeeMaster, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
    Task<bool> EmployeeCodeExistsAsync(string employeeCode, CancellationToken cancellationToken = default);
    Task<bool> EnrollmentCodeExistsAsync(string enrollmentCode, CancellationToken cancellationToken = default);
}
