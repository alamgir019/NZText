using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IDesignationRepository
{
    Task<List<Designation>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<Designation?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(Designation designation, CancellationToken cancellationToken = default);
    Task UpdateAsync(Designation designation, CancellationToken cancellationToken = default);
    Task DeleteAsync(Designation designation, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
