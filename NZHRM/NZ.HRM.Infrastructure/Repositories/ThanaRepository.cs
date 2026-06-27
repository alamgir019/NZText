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
}
