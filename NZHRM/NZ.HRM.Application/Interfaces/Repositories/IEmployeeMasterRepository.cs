using NZ.HRM.Application.Employees.Queries.GetEmployeeMasterList;
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
    Task<List<HrmEmployeeMaster>> GetByStatusAsync(string status, CancellationToken cancellationToken = default);
    // Get employees by status up to a specified UTC date (inclusive). When includeInactive is false, only active employees are returned.
    Task<List<HrmEmployeeMaster>> GetByStatusUpToDateAsync(Employees.Queries.GetItActivationSummary.GetItActivationSummaryQuery query, CancellationToken cancellationToken = default);
    Task<List<HrmEmployeeMaster>> SearchAsync(string searchText, CancellationToken cancellationToken = default);
    Task<List<HrmEmployeeMaster>> GetByDateAsync(DateTime onDatel, bool includeInactive = false, CancellationToken cancellationToken = default);
    /// <summary>
    /// Get employee master list with advanced filtering and pagination at database level.
    /// </summary>
    Task<(List<HrmEmployeeMaster> employees, int totalCount)> GetEmployeeMasterListAsync(
        EmployeeMasterListFilterRequest filterRequest,
        CancellationToken cancellationToken = default);
    Task<string> AddAsync(HrmEmployeeMaster employeeMaster, CancellationToken cancellationToken = default);
    Task UpdateAsync(HrmEmployeeMaster employeeMaster, CancellationToken cancellationToken = default);
    Task UpdateRangeAsync(List<HrmEmployeeMaster> employeeMasters, CancellationToken cancellationToken = default);
    Task DeleteAsync(HrmEmployeeMaster employeeMaster, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
    Task<bool> EmployeeCodeExistsAsync(string employeeCode, CancellationToken cancellationToken = default);
    Task<bool> EnrollmentCodeExistsAsync(string enrollmentCode, CancellationToken cancellationToken = default);
    // Generate a next unique enrollment id for the provided date (caller should pass UTC date).
    // Implementations should ensure uniqueness under concurrent callers.
    Task<string> GetNextEnrollmentIdAsync(DateTime todayUtc, CancellationToken cancellationToken = default);
}
