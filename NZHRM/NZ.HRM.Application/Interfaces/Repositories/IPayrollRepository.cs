using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IPayrollRepository
{
    Task<List<HrmEmployeePayroll>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<List<HrmEmployeePayroll>> GetByEmployeeIdAsync(string employeeId, bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<HrmEmployeePayroll?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(HrmEmployeePayroll financialDetail, CancellationToken cancellationToken = default);
    Task UpdateAsync(HrmEmployeePayroll financialDetail, CancellationToken cancellationToken = default);
    Task DeleteAsync(HrmEmployeePayroll financialDetail, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
    Task<IEnumerable<string>> UpdateRangeAsync(List<HrmEmployeePayroll> payrolls, CancellationToken cancellationToken);
}
