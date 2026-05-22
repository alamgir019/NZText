using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class SectionCellRepository : ISectionCellRepository
{
    private readonly ApplicationDbContext _context;

    public SectionCellRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<SectionCell>> GetAllAsync(bool includeInactive = false, string? sectionId = null, string? cellId = null, CancellationToken cancellationToken = default)
    {
        var query = _context.SectionCells
            .Include(sc => sc.Section)
            .Include(sc => sc.Cell)
            .AsQueryable();

        if (!includeInactive)
            query = query.Where(sc => sc.IsActive);

        if (!string.IsNullOrWhiteSpace(sectionId))
            query = query.Where(sc => sc.SectionId == sectionId);

        if (!string.IsNullOrWhiteSpace(cellId))
            query = query.Where(sc => sc.CellId == cellId);

        return await query
            .OrderBy(sc => sc.Section != null ? sc.Section.SectionName : string.Empty)
            .ThenBy(sc => sc.Cell != null ? sc.Cell.NameEnglish : string.Empty)
            .ToListAsync(cancellationToken);
    }

    public async Task<SectionCell?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.SectionCells
            .Include(sc => sc.Section)
            .Include(sc => sc.Cell)
            .FirstOrDefaultAsync(sc => sc.Id == id && sc.IsActive, cancellationToken);
    }

    public async Task<string?> GetSectionIdByCellIdAsync(string cellId, CancellationToken cancellationToken = default)
    {
        return await _context.SectionCells
            .Where(sc => sc.CellId == cellId)
            .Select(sc => sc.SectionId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<string?> GetSectionNameByCellIdAsync(string cellId, CancellationToken cancellationToken = default)
    {
        return await _context.SectionCells
            .Where(sc => sc.CellId == cellId)
            .Select(sc => sc.Section != null ? sc.Section.SectionName : null)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task SetSectionForCellAsync(string cellId, string sectionId, CancellationToken cancellationToken = default)
    {
        var existingMappings = await _context.SectionCells
            .Where(sc => sc.CellId == cellId)
            .ToListAsync(cancellationToken);

        if (existingMappings.Count > 0)
        {
            _context.SectionCells.RemoveRange(existingMappings);
        }

        _context.SectionCells.Add(new SectionCell
        {
            CellId = cellId,
            SectionId = sectionId,
            IsActive = true
        });

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<string> AddAsync(SectionCell sectionCell, CancellationToken cancellationToken = default)
    {
        _context.SectionCells.Add(sectionCell);
        await _context.SaveChangesAsync(cancellationToken);
        return sectionCell.Id;
    }

    public async Task UpdateAsync(SectionCell sectionCell, CancellationToken cancellationToken = default)
    {
        _context.SectionCells.Update(sectionCell);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(SectionCell sectionCell, CancellationToken cancellationToken = default)
    {
        _context.SectionCells.Remove(sectionCell);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.SectionCells
            .AnyAsync(sc => sc.Id == id, cancellationToken);
    }
}
