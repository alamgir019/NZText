using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IDivisionRepository
{
    Task<List<LookDivision>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<LookDivision?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
}
