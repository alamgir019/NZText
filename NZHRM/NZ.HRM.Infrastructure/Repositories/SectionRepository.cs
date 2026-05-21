using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class SectionRepository : ISectionRepository
{
    private readonly ApplicationDbContext _context;

    public SectionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Section>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.Sections.AsQueryable();

        if (!includeInactive)
        {
            query = query.Where(s => s.IsActive);
        }

        return await query
            .OrderBy(s => s.SectionName)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Section>> GetByDepartmentIdAsync(string departmentId, bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.DepartmentSections
            .Where(ds => ds.DepartmentId == departmentId)
            .Select(ds => ds.Section!)
            .AsQueryable();

        if (!includeInactive)
        {
            query = query.Where(s => s.IsActive);
        }

        return await query
            .OrderBy(s => s.SectionName)
            .Distinct()
            .ToListAsync(cancellationToken);
    }

    public async Task<Section?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Sections
            .FirstOrDefaultAsync(s => s.Id == id && s.IsActive, cancellationToken);
    }

    public async Task<string> AddAsync(Section section, CancellationToken cancellationToken = default)
    {
        _context.Sections.Add(section);
        await _context.SaveChangesAsync(cancellationToken);
        return section.Id;
    }

    public async Task UpdateAsync(Section section, CancellationToken cancellationToken = default)
    {
        _context.Sections.Update(section);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Section section, CancellationToken cancellationToken = default)
    {
        _context.Sections.Remove(section);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Sections
            .AnyAsync(s => s.Id == id, cancellationToken);
    }
}
