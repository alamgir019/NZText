using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IRawPunchRepository
{
    Task<string> AddAsync(AttRawPunch rawPunch, CancellationToken cancellationToken = default);
    Task<AttRawPunch?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<List<AttRawPunch>> GetByEmployeeIdAndDateAsync(string employeeId, DateOnly date, CancellationToken cancellationToken = default);
}
