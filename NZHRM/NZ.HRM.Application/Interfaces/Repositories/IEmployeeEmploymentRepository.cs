using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IEmployeeEmploymentRepository
{
    Task<List<HrmEmployeeEmployment>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<HrmEmployeeEmployment?> GetByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken = default);
    Task<HrmEmployeeEmployment?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(HrmEmployeeEmployment employeeEmployment, CancellationToken cancellationToken = default);
    Task UpdateAsync(HrmEmployeeEmployment employeeEmployment, CancellationToken cancellationToken = default);
    Task DeleteAsync(HrmEmployeeEmployment employeeEmployment, CancellationToken cancellationToken = default);
    Task<bool> ExistsForEmployeeAsync(string employeeId, CancellationToken cancellationToken = default);
}