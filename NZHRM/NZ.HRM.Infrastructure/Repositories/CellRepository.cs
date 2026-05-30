using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class CellRepository : ICellRepository
{
    private readonly ApplicationDbContext _context;

    public CellRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Cell>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.Set<Cell>().AsQueryable();

        if (!includeInactive)
            query = query.Where(c => c.IsActive);

        return await query.OrderBy(c => c.NameEnglish).ToListAsync(cancellationToken);
    }

    public async Task<Cell?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Cell>()
            .FirstOrDefaultAsync(c => c.Id == id && c.IsActive, cancellationToken);
    }

    public async Task<List<Cell>> GetBySectionIdAsync(string sectionId, bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.SectionCells
            .Where(sc => sc.SectionId == sectionId)
            .Select(sc => sc.Cell!)
            .AsQueryable();

        if (!includeInactive)
        {
            query = query.Where(c => c.IsActive);
        }

        return await query
            .OrderBy(c => c.NameEnglish)
            .Distinct()
            .ToListAsync(cancellationToken);
    }

    public async Task<string> AddAsync(Cell cell, CancellationToken cancellationToken = default)
    {
        _context.Set<Cell>().Add(cell);
        await _context.SaveChangesAsync(cancellationToken);
        return cell.Id;
    }

    public async Task UpdateAsync(Cell cell, CancellationToken cancellationToken = default)
    {
        _context.Set<Cell>().Update(cell);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Cell cell, CancellationToken cancellationToken = default)
    {
        _context.Set<Cell>().Remove(cell);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Cell>().AnyAsync(c => c.Id == id, cancellationToken);
    }
}
