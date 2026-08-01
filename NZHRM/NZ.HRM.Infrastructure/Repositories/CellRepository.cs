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

    public async Task<List<MstCell>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.Set<MstCell>().AsQueryable();
        query = query.Include(c => c.Section);
        if (!includeInactive)
            query = query.Where(c => c.IsActive);

        return await query.OrderBy(c => c.SortOrder).ThenBy(c => c.CellName).ToListAsync(cancellationToken);
    }

    public async Task<MstCell?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<MstCell>()
            .Include(c => c.Section)
            .FirstOrDefaultAsync(c => c.Id == id && c.IsActive, cancellationToken);
    }

    public async Task<List<MstCell>> GetBySectionIdAsync(string sectionId, bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.MstCells
            .Where(sc => sc.SectionId == sectionId)
            .Include(c => c.Section)
            .AsQueryable();

        if (!includeInactive)
        {
            query = query.Where(c => c.IsActive);
        }

        return await query
            .OrderBy(c => c.SortOrder)
            .ThenBy(c => c.CellName)
            .Distinct()
            .ToListAsync(cancellationToken);
    }

    public async Task<string> AddAsync(MstCell cell, CancellationToken cancellationToken = default)
    {
        _context.Set<MstCell>().Add(cell);
        await _context.SaveChangesAsync(cancellationToken);
        return cell.Id;
    }

    public async Task UpdateAsync(MstCell cell, CancellationToken cancellationToken = default)
    {
        _context.Set<MstCell>().Update(cell);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MstCell cell, CancellationToken cancellationToken = default)
    {
        _context.Set<MstCell>().Remove(cell);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<MstCell>().AnyAsync(c => c.Id == id, cancellationToken);
    }
}
