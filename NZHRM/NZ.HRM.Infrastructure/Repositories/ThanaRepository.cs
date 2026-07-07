using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class ThanaRepository : IThanaRepository
{
    private readonly ApplicationDbContext _context;

    public ThanaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<LookThana>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Thanas
            .Include(t => t.District)
            .Where(t => t.IsActive)
            .OrderBy(t => t.ThanaName)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<LookThana>> GetByDistrictIdAsync(string districtId, CancellationToken cancellationToken = default)
    {
        return await _context.Thanas
            .Include(t => t.District)
            .Where(t => t.DistrictId == districtId && t.IsActive)
            .OrderBy(t => t.ThanaName)
            .ToListAsync(cancellationToken);
    }

    public async Task<string> AddAsync(LookThana thana, CancellationToken cancellationToken = default)
    {
        _context.Thanas.Add(thana);
        await _context.SaveChangesAsync(cancellationToken);
        return thana.Id;
    }

    public async Task UpdateAsync(LookThana thana, CancellationToken cancellationToken = default)
    {
        _context.Thanas.Update(thana);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(LookThana thana, CancellationToken cancellationToken = default)
    {
        _context.Thanas.Remove(thana);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<LookThana?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Thanas.FirstOrDefaultAsync(t => t.Id == id && t.IsActive, cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Thanas.AnyAsync(t => t.Id == id, cancellationToken);
    }
}
