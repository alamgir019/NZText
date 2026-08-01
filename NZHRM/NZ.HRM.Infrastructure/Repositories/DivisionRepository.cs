using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class DivisionRepository : IDivisionRepository
{
    private readonly ApplicationDbContext _context;

    public DivisionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<LookDivision>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Divisions
            .Where(d => d.IsActive)
            .OrderBy(d => d.SortOrder)
            .ThenBy(d => d.DivisionName)
            .ToListAsync(cancellationToken);
    }

    public async Task<LookDivision?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Divisions
            .FirstOrDefaultAsync(d => d.Id == id && d.IsActive, cancellationToken);
    }

    public async Task<string> AddAsync(LookDivision division, CancellationToken cancellationToken = default)
    {
        _context.Divisions.Add(division);
        await _context.SaveChangesAsync(cancellationToken);
        return division.Id;
    }

    public async Task UpdateAsync(LookDivision division, CancellationToken cancellationToken = default)
    {
        _context.Divisions.Update(division);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(LookDivision division, CancellationToken cancellationToken = default)
    {
        _context.Divisions.Remove(division);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Divisions.AnyAsync(d => d.Id == id, cancellationToken);
    }
}
