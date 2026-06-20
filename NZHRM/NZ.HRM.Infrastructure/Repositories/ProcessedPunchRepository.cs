using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class ProcessedPunchRepository : IProcessedPunchRepository
{
    private readonly ApplicationDbContext _context;

    public ProcessedPunchRepository(ApplicationDbContext context)
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
