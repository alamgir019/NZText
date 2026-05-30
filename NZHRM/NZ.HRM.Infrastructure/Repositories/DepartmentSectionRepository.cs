using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class DepartmentSectionRepository : IDepartmentSectionRepository
{
    private readonly ApplicationDbContext _context;

    public DepartmentSectionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<DepartmentSection>> GetAllAsync(bool includeInactive = false, string? departmentId = null, string? sectionId = null, CancellationToken cancellationToken = default)
    {
        var query = _context.DepartmentSections
            .Include(ds => ds.Department)
            .Include(ds => ds.Section)
            .AsQueryable();

        if (!includeInactive)
            query = query.Where(ds => ds.IsActive);

        if (!string.IsNullOrWhiteSpace(departmentId))
            query = query.Where(ds => ds.DepartmentId == departmentId);

        if (!string.IsNullOrWhiteSpace(sectionId))
            query = query.Where(ds => ds.SectionId == sectionId);

        return await query
            .OrderBy(ds => ds.Department != null ? ds.Department.DepartmentName : string.Empty)
            .ThenBy(ds => ds.Section != null ? ds.Section.SectionName : string.Empty)
            .ToListAsync(cancellationToken);
    }

    public async Task<DepartmentSection?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.DepartmentSections
            .Include(ds => ds.Department)
            .Include(ds => ds.Section)
            .FirstOrDefaultAsync(ds => ds.Id == id && ds.IsActive, cancellationToken);
    }

    public async Task<string?> GetDepartmentIdBySectionIdAsync(string sectionId, CancellationToken cancellationToken = default)
    {
        return await _context.DepartmentSections
            .Where(ds => ds.SectionId == sectionId)
            .Select(ds => ds.DepartmentId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<string?> GetDepartmentNameBySectionIdAsync(string sectionId, CancellationToken cancellationToken = default)
    {
        return await _context.DepartmentSections
            .Where(ds => ds.SectionId == sectionId)
            .Select(ds => ds.Department != null ? ds.Department.DepartmentName : null)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task SetDepartmentForSectionAsync(string sectionId, string departmentId, CancellationToken cancellationToken = default)
    {
        var existingMappings = await _context.DepartmentSections
            .Where(ds => ds.SectionId == sectionId)
            .ToListAsync(cancellationToken);

        if (existingMappings.Count > 0)
        {
            _context.DepartmentSections.RemoveRange(existingMappings);
        }

        _context.DepartmentSections.Add(new DepartmentSection
        {
            SectionId = sectionId,
            DepartmentId = departmentId,
            IsActive = true
        });

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<string> AddAsync(DepartmentSection departmentSection, CancellationToken cancellationToken = default)
    {
        _context.DepartmentSections.Add(departmentSection);
        await _context.SaveChangesAsync(cancellationToken);
        return departmentSection.Id;
    }

    public async Task UpdateAsync(DepartmentSection departmentSection, CancellationToken cancellationToken = default)
    {
        _context.DepartmentSections.Update(departmentSection);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DepartmentSection departmentSection, CancellationToken cancellationToken = default)
    {
        _context.DepartmentSections.Remove(departmentSection);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.DepartmentSections
            .AnyAsync(ds => ds.Id == id, cancellationToken);
    }
}
