using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IThanaRepository
{
    Task<List<LookThana>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<LookThana>> GetByDistrictIdAsync(string districtId, CancellationToken cancellationToken = default);
    Task<string> AddAsync(LookThana thana, CancellationToken cancellationToken = default);
    Task UpdateAsync(LookThana thana, CancellationToken cancellationToken = default);
    Task DeleteAsync(LookThana thana, CancellationToken cancellationToken = default);
    Task<LookThana?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
