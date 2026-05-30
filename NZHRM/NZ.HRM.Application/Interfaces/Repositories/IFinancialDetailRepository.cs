using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IFinancialDetailRepository
{
    Task<List<FinancialDetail>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<List<FinancialDetail>> GetByEmployeeIdAsync(string employeeId, bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<FinancialDetail?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(FinancialDetail financialDetail, CancellationToken cancellationToken = default);
    Task UpdateAsync(FinancialDetail financialDetail, CancellationToken cancellationToken = default);
    Task DeleteAsync(FinancialDetail financialDetail, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
