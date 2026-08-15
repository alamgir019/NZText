using Microsoft.EntityFrameworkCore;
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
}
