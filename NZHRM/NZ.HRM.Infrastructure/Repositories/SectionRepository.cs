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

    public async Task<List<MstSection>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.MstSections.AsQueryable();

        if (!includeInactive)
        {
            query = query.Where(s => s.IsActive);
        }

        return await query
            .OrderBy(s => s.SortOrder)
            .ThenBy(s => s.SectionName)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<MstSection>> GetByDepartmentIdAsync(string departmentId, bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.MstSections
            .Where(ds => ds.DepartmentId == departmentId)
            .Include(ds => ds.Department)
            .Select(ds => ds)
            .AsQueryable();

        if (!includeInactive)
        {
            query = query.Where(s => s.IsActive);
        }

        return await query
            .OrderBy(s => s.SortOrder)
            .ThenBy(s => s.SectionName)
            .Distinct()
            .ToListAsync(cancellationToken);
    }

    public async Task<MstSection?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.MstSections
            .FirstOrDefaultAsync(s => s.Id == id && s.IsActive, cancellationToken);
    }

    public async Task<string> AddAsync(MstSection section, CancellationToken cancellationToken = default)
    {
        _context.MstSections.Add(section);
        await _context.SaveChangesAsync(cancellationToken);
        return section.Id;
    }

    public async Task UpdateAsync(MstSection section, CancellationToken cancellationToken = default)
    {
        _context.MstSections.Update(section);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MstSection section, CancellationToken cancellationToken = default)
    {
        _context.MstSections.Remove(section);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.MstSections
            .AnyAsync(s => s.Id == id, cancellationToken);
    }
}
