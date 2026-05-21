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
}
