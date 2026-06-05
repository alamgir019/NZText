using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class LocationDepartmentRepository : ILocationDepartmentRepository
{
    private readonly ApplicationDbContext _context;

    public LocationDepartmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<MstSubunitDepartment>> GetAllAsync(bool includeInactive = false, string? locationId = null, string? departmentId = null, CancellationToken cancellationToken = default)
    {
        var query = _context.MstSubunitDepartments
            .Include(ld => ld.Subunit)
            .Include(ld => ld.Department)
            .AsQueryable();

        if (!includeInactive)
            query = query.Where(ld => ld.IsActive);

        if (!string.IsNullOrWhiteSpace(locationId))
            query = query.Where(ld => ld.SubunitId == locationId);

        if (!string.IsNullOrWhiteSpace(departmentId))
            query = query.Where(ld => ld.DepartmentId == departmentId);

        return await query
            .OrderBy(ld => ld.Subunit != null ? ld.Subunit.SubunitName : string.Empty)
            .ThenBy(ld => ld.Department != null ? ld.Department.DepartmentName : string.Empty)
            .ToListAsync(cancellationToken);
    }

    public async Task<MstSubunitDepartment?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.MstSubunitDepartments
            .Include(ld => ld.Subunit)
            .Include(ld => ld.Department)
            .FirstOrDefaultAsync(ld => ld.Id == id && ld.IsActive, cancellationToken);
    }

    public async Task<string?> GetLocationIdByDepartmentIdAsync(string departmentId, CancellationToken cancellationToken = default)
    {
        return await _context.MstSubunitDepartments
            .Where(ld => ld.DepartmentId == departmentId)
            .Select(ld => ld.SubunitId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<string?> GetLocationNameByDepartmentIdAsync(string departmentId, CancellationToken cancellationToken = default)
    {
        return await _context.MstSubunitDepartments
            .Where(ld => ld.DepartmentId == departmentId)
            .Select(ld => ld.Subunit != null ? ld.Subunit.SubunitName : null)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task SetLocationForDepartmentAsync(string departmentId, string locationId, CancellationToken cancellationToken = default)
    {
        var existingMappings = await _context.MstSubunitDepartments
            .Where(ld => ld.DepartmentId == departmentId)
            .ToListAsync(cancellationToken);

        if (existingMappings.Count > 0)
            _context.MstSubunitDepartments.RemoveRange(existingMappings);

        _context.MstSubunitDepartments.Add(new MstSubunitDepartment
        {
            DepartmentId = departmentId,
            SubunitId = locationId,
            IsActive = true
        });

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<string> AddAsync(MstSubunitDepartment locationDepartment, CancellationToken cancellationToken = default)
    {
        _context.MstSubunitDepartments.Add(locationDepartment);
        await _context.SaveChangesAsync(cancellationToken);
        return locationDepartment.Id;
    }

    public async Task UpdateAsync(MstSubunitDepartment locationDepartment, CancellationToken cancellationToken = default)
    {
        _context.MstSubunitDepartments.Update(locationDepartment);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MstSubunitDepartment locationDepartment, CancellationToken cancellationToken = default)
    {
        _context.MstSubunitDepartments.Remove(locationDepartment);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.MstSubunitDepartments.AnyAsync(ld => ld.Id == id, cancellationToken);
    }
}
