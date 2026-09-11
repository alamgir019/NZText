using NZ.Attendance.Application.RawPunches.Queries.GetProcessedPunches;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IProcessedPunchRepository
{
    Task<string> AddAsync(AttProcessedPunch processedPunch, CancellationToken cancellationToken = default);
    Task<AttProcessedPunch?> GetByRawPunchIdAsync(string rawPunchId, CancellationToken cancellationToken = default);
    Task<List<AttProcessedPunch>> GetByEmployeeIdAndDateAsync(string employeeId, DateOnly date, CancellationToken cancellationToken = default);

    Task<List<ProcessedPunchListItemDto>> GetProcessedPunchesAsync(
        string shiftId,
        string companyId,
        string punchType,
        CancellationToken cancellationToken = default);
}
