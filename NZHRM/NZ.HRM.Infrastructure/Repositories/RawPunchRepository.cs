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

    public async Task<string> AddAsync(AttRawPunch rawPunch, CancellationToken cancellationToken = default)
    {
        _context.AttRawPunches.Add(rawPunch);
        await _context.SaveChangesAsync(cancellationToken);
        return rawPunch.Id;
    }

    public async Task<AttRawPunch?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.AttRawPunches
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<AttRawPunch>> GetByEmployeeIdAndDateAsync(string employeeId, DateOnly date, CancellationToken cancellationToken = default)
    {
        return await _context.AttRawPunches
            .Where(x => x.EmployeeId == employeeId && x.PunchDate == date)
            .OrderBy(x => x.PunchTime)
            .ToListAsync(cancellationToken);
    }
}
