using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IThanaRepository
{
    Task<List<LookThana>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<LookThana>> GetByDistrictIdAsync(string districtId, CancellationToken cancellationToken = default);
}
