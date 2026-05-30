using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IRawPunchRepository
{
    Task<string> AddAsync(RawPunch rawPunch, CancellationToken cancellationToken = default);
    Task<RawPunch?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<List<RawPunch>> GetByEmployeeIdAndDateAsync(string employeeId, DateTime date, CancellationToken cancellationToken = default);
}
