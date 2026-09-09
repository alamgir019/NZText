using NZ.Attendance.Application.RawPunches.Queries.GetProcessedPunches;
using NZ.HRM.Application.Interfaces.Repositories;

namespace NZ.Attendance.Application.RawPunches.Handlers;

public class QueryHandler
{
    private readonly IProcessedPunchRepository _processedPunchRepository;

    public QueryHandler(IProcessedPunchRepository processedPunchRepository)
    {
        _processedPunchRepository = processedPunchRepository;
    }

    public async Task<List<ProcessedPunchListItemDto>> Handle(
        GetProcessedPunchesQuery query,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query.ShiftId))
            throw new ArgumentException("ShiftId is required");

        if (string.IsNullOrWhiteSpace(query.CompanyId))
            throw new ArgumentException("CompanyId is required");

        if (string.IsNullOrWhiteSpace(query.PunchType))
            throw new ArgumentException("PunchType is required");

        return await _processedPunchRepository.GetProcessedPunchesAsync(
            query.ShiftId,
            query.CompanyId,
            query.PunchType,
            cancellationToken);
    }
}