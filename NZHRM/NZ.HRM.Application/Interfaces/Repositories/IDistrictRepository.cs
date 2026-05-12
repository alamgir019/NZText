using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IDistrictRepository
{
    Task<List<District>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<District>> GetByDivisionIdAsync(string divisionId, CancellationToken cancellationToken = default);
}
