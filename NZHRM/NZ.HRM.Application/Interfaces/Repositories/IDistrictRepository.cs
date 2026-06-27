using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IDistrictRepository
{
    Task<List<LookDistrict>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<LookDistrict>> GetByDivisionIdAsync(string divisionId, CancellationToken cancellationToken = default);
}
