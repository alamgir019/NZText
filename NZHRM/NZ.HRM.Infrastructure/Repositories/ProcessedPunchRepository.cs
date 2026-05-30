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

    public async Task<string> AddAsync(ProcessedPunch processedPunch, CancellationToken cancellationToken = default)
    {
        _context.ProcessedPunches.Add(processedPunch);
        await _context.SaveChangesAsync(cancellationToken);
        return processedPunch.Id;
    }

    public async Task<ProcessedPunch?> GetByRawPunchIdAsync(string rawPunchId, CancellationToken cancellationToken = default)
    {
        return await _context.ProcessedPunches
            .FirstOrDefaultAsync(x => x.RawPunchId == rawPunchId, cancellationToken);
    }

    public async Task<List<ProcessedPunch>> GetByEmployeeIdAndDateAsync(string employeeId, DateTime date, CancellationToken cancellationToken = default)
    {
        return await _context.ProcessedPunches
            .Where(x => x.EmployeeId == employeeId && x.PunchDate.Date == date.Date)
            .OrderBy(x => x.AdjustedPunchTime)
            .ToListAsync(cancellationToken);
    }
}
