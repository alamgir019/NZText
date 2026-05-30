using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class RawPunchRepository : IRawPunchRepository
{
    private readonly ApplicationDbContext _context;

    public RawPunchRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> AddAsync(RawPunch rawPunch, CancellationToken cancellationToken = default)
    {
        _context.RawPunches.Add(rawPunch);
        await _context.SaveChangesAsync(cancellationToken);
        return rawPunch.Id;
    }

    public async Task<RawPunch?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.RawPunches
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<RawPunch>> GetByEmployeeIdAndDateAsync(string employeeId, DateTime date, CancellationToken cancellationToken = default)
    {
        return await _context.RawPunches
            .Where(x => x.EmployeeId == employeeId && x.PunchDate.Date == date.Date)
            .OrderBy(x => x.PunchTime)
            .ToListAsync(cancellationToken);
    }
}
