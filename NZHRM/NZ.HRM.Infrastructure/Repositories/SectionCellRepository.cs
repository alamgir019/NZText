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
}
