using Microsoft.EntityFrameworkCore;
using NZ.Attendance.Application.RawPunches.Queries.GetProcessedPunches;
using NZ.Attendance.Infrastructure.Persistence;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;

namespace NZ.Attendance.Infrastructure.Repositories;

public class AttProcessedPunchRepository : IProcessedPunchRepository
{
    private readonly AttendanceDbContext _context;

    public AttProcessedPunchRepository(AttendanceDbContext context)
    {
        _context = context;
    }

    public async Task<string> AddAsync(AttProcessedPunch processedPunch, CancellationToken cancellationToken = default)
    {
        _context.AttProcessedPunches.Add(processedPunch);
        await _context.SaveChangesAsync(cancellationToken);
        return processedPunch.Id;
    }

    public async Task<AttProcessedPunch?> GetByRawPunchIdAsync(string rawPunchId, CancellationToken cancellationToken = default)
    {
        return await _context.AttProcessedPunches
            .FirstOrDefaultAsync(x => x.RawPunchId == rawPunchId, cancellationToken);
    }

    public async Task<List<AttProcessedPunch>> GetByEmployeeIdAndDateAsync(string employeeId, DateOnly date, CancellationToken cancellationToken = default)
    {
        return await _context.AttProcessedPunches
            .Where(x => x.EmployeeId == employeeId && x.PunchDate == date)
            .OrderBy(x => x.AdjustedPunchTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<ProcessedPunchListItemDto>> GetProcessedPunchesAsync(
        string shiftId,
        string companyId,
        string punchType,
        CancellationToken cancellationToken = default)
    {
        var normalizedPunchType = punchType.Trim();

        var result = await (
            from processedPunch in _context.AttProcessedPunches.AsNoTracking()
            join employee in _context.HrmEmployeeMasters.AsNoTracking()
                on processedPunch.EmployeeId equals employee.Id
            join employment in _context.HrmEmployeeEmployments.AsNoTracking()
                on processedPunch.EmployeeId equals employment.EmployeeId
            where processedPunch.ShiftId == shiftId
                && employment.UnitId == companyId
                && processedPunch.PunchType == normalizedPunchType
            orderby processedPunch.PunchDate, processedPunch.AdjustedPunchTime
            select new ProcessedPunchListItemDto
            {
                EmployeeId = processedPunch.EmployeeId,
                EmployeeName = employee.EmployeeName,
                EmployeePhotoUrl = _context.HrmEmployeeDocuments
                    .Where(document =>
                        document.EmployeeId == processedPunch.EmployeeId
                        && (document.DocumentType == "Photo" || document.DocumentType == "PassportPhoto"))
                    .OrderByDescending(document => document.CreatedOn)
                    .Select(document => document.FilePath)
                    .FirstOrDefault(),
                PunchDate = processedPunch.PunchDate,
                AdjustedPunchTime = processedPunch.AdjustedPunchTime,
                RawPunchTime = processedPunch.RawPunchTime,
                PunchType = processedPunch.PunchType
            })
            .ToListAsync(cancellationToken);

        return result;
    }
}
