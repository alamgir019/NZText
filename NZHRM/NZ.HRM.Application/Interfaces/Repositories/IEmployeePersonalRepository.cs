using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IEmployeePersonalRepository
{
    Task<List<HrmEmployeePersonal>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<HrmEmployeePersonal?> GetByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken = default);
    Task<HrmEmployeePersonal?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(HrmEmployeePersonal employeePersonal, CancellationToken cancellationToken = default);
    Task UpdateAsync(HrmEmployeePersonal employeePersonal, CancellationToken cancellationToken = default);
    Task DeleteAsync(HrmEmployeePersonal employeePersonal, CancellationToken cancellationToken = default);
    Task<bool> ExistsForEmployeeAsync(string employeeId, CancellationToken cancellationToken = default);
}
