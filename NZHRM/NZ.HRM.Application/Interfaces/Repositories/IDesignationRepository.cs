using NZ.HRM.Application.Designations.Queries.GetAllDesignations;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IDesignationRepository
{
    Task<MstDesignation?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(MstDesignation designation, CancellationToken cancellationToken = default);
    Task UpdateAsync(MstDesignation designation, CancellationToken cancellationToken = default);
    Task DeleteAsync(MstDesignation designation, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
    Task<IEnumerable<MstDesignation>> GetAllAsync(GetAllDesignationsQuery query, CancellationToken cancellationToken);
}
