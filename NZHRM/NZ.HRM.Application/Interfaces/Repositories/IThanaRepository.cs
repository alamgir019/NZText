using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IThanaRepository
{
    Task<List<Thana>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<Thana>> GetByDistrictIdAsync(string districtId, CancellationToken cancellationToken = default);
}
