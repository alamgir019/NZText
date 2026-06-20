using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IEmployeeMasterRepository
{
    // If onDate is provided, returns employees relevant to that date (e.g., created on that date). If null returns all.
    Task<List<HrmEmployeeMaster>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<HrmEmployeeMaster?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<HrmEmployeeMaster?> GetByEmployeeCodeAsync(string employeeCode, CancellationToken cancellationToken = default);
    Task<List<HrmEmployeeMaster>> GetByCompanyIdAsync(string companyId, CancellationToken cancellationToken = default);
    Task<List<HrmEmployeeMaster>> GetByDepartmentIdAsync(string departmentId, CancellationToken cancellationToken = default);
    Task<List<HrmEmployeeMaster>> SearchAsync(string searchText, CancellationToken cancellationToken = default);
    Task<List<HrmEmployeeMaster>> GetByDateAsync(DateTime onDatel, bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<string> AddAsync(HrmEmployeeMaster employeeMaster, CancellationToken cancellationToken = default);
    Task UpdateAsync(HrmEmployeeMaster employeeMaster, CancellationToken cancellationToken = default);
    Task DeleteAsync(HrmEmployeeMaster employeeMaster, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
    Task<bool> EmployeeCodeExistsAsync(string employeeCode, CancellationToken cancellationToken = default);
    Task<bool> EnrollmentCodeExistsAsync(string enrollmentCode, CancellationToken cancellationToken = default);
}
