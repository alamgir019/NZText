using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface ICompanyRepository
{
    Task<List<Company>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<Company?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(Company company, CancellationToken cancellationToken = default);
    Task UpdateAsync(Company company, CancellationToken cancellationToken = default);
    Task DeleteAsync(Company company, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}