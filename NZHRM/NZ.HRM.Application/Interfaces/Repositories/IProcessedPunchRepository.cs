using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IProcessedPunchRepository
{
    Task<string> AddAsync(ProcessedPunch processedPunch, CancellationToken cancellationToken = default);
    Task<ProcessedPunch?> GetByRawPunchIdAsync(string rawPunchId, CancellationToken cancellationToken = default);
    Task<List<ProcessedPunch>> GetByEmployeeIdAndDateAsync(string employeeId, DateTime date, CancellationToken cancellationToken = default);
}
