using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class DistrictRepository : IDistrictRepository
{
    private readonly ApplicationDbContext _context;

    public DistrictRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<District>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Districts
            .Include(d => d.Division)
            .Where(d => d.IsActive)
            .OrderBy(d => d.DistrictName)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<District>> GetByDivisionIdAsync(string divisionId, CancellationToken cancellationToken = default)
    {
        return await _context.Districts
            .Include(d => d.Division)
            .Where(d => d.DivisionId == divisionId && d.IsActive)
            .OrderBy(d => d.DistrictName)
            .ToListAsync(cancellationToken);
    }
}
