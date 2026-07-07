using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IDistrictRepository
{
    Task<List<LookDistrict>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<LookDistrict>> GetByDivisionIdAsync(string divisionId, CancellationToken cancellationToken = default);
    Task<string> AddAsync(LookDistrict district, CancellationToken cancellationToken = default);
    Task UpdateAsync(LookDistrict district, CancellationToken cancellationToken = default);
    Task DeleteAsync(LookDistrict district, CancellationToken cancellationToken = default);
    Task<LookDistrict?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
