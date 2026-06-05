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

    public async Task<List<MstDepartmentSection>> GetAllAsync(bool includeInactive = false, string? departmentId = null, string? sectionId = null, CancellationToken cancellationToken = default)
    {
        var query = _context.MstDepartmentSections
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

    public async Task<MstDepartmentSection?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.MstDepartmentSections
            .Include(ds => ds.Department)
            .Include(ds => ds.Section)
            .FirstOrDefaultAsync(ds => ds.Id == id && ds.IsActive, cancellationToken);
    }

    public async Task<string?> GetDepartmentIdBySectionIdAsync(string sectionId, CancellationToken cancellationToken = default)
    {
        return await _context.MstDepartmentSections
            .Where(ds => ds.SectionId == sectionId)
            .Select(ds => ds.DepartmentId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<string?> GetDepartmentNameBySectionIdAsync(string sectionId, CancellationToken cancellationToken = default)
    {
        return await _context.MstDepartmentSections
            .Where(ds => ds.SectionId == sectionId)
            .Select(ds => ds.Department != null ? ds.Department.DepartmentName : null)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task SetDepartmentForSectionAsync(string sectionId, string departmentId, CancellationToken cancellationToken = default)
    {
        var existingMappings = await _context.MstDepartmentSections
            .Where(ds => ds.SectionId == sectionId)
            .ToListAsync(cancellationToken);

        if (existingMappings.Count > 0)
        {
            _context.MstDepartmentSections.RemoveRange(existingMappings);
        }

        _context.MstDepartmentSections.Add(new MstDepartmentSection
        {
            SectionId = sectionId,
            DepartmentId = departmentId,
            IsActive = true
        });

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<string> AddAsync(MstDepartmentSection departmentSection, CancellationToken cancellationToken = default)
    {
        _context.MstDepartmentSections.Add(departmentSection);
        await _context.SaveChangesAsync(cancellationToken);
        return departmentSection.Id;
    }

    public async Task UpdateAsync(MstDepartmentSection departmentSection, CancellationToken cancellationToken = default)
    {
        _context.MstDepartmentSections.Update(departmentSection);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MstDepartmentSection departmentSection, CancellationToken cancellationToken = default)
    {
        _context.MstDepartmentSections.Remove(departmentSection);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.MstDepartmentSections
            .AnyAsync(ds => ds.Id == id, cancellationToken);
    }
}
