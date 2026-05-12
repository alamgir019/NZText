using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IDivisionRepository
{
    Task<List<Division>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Division?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
}
