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

    public async Task<List<LookDistrict>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Districts
            .Include(d => d.Division)
            .Where(d => d.IsActive)
            .OrderBy(d => d.SortOrder)
            .ThenBy(d => d.DistrictName)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<LookDistrict>> GetByDivisionIdAsync(string divisionId, CancellationToken cancellationToken = default)
    {
        return await _context.Districts
            .Include(d => d.Division)
            .Where(d => d.DivisionId == divisionId && d.IsActive)
            .OrderBy(d => d.DistrictName)
            .ToListAsync(cancellationToken);
    }

    public async Task<string> AddAsync(LookDistrict district, CancellationToken cancellationToken = default)
    {
        _context.Districts.Add(district);
        await _context.SaveChangesAsync(cancellationToken);
        return district.Id;
    }

    public async Task UpdateAsync(LookDistrict district, CancellationToken cancellationToken = default)
    {
        _context.Districts.Update(district);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(LookDistrict district, CancellationToken cancellationToken = default)
    {
        _context.Districts.Remove(district);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<LookDistrict?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Districts.FirstOrDefaultAsync(d => d.Id == id && d.IsActive, cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Districts.AnyAsync(d => d.Id == id, cancellationToken);
    }
}
