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

    public async Task<List<LocationDepartment>> GetAllAsync(bool includeInactive = false, string? locationId = null, string? departmentId = null, CancellationToken cancellationToken = default)
    {
        var query = _context.LocationDepartments
            .Include(ld => ld.Location)
            .Include(ld => ld.Department)
            .AsQueryable();

        if (!includeInactive)
            query = query.Where(ld => ld.IsActive);

        if (!string.IsNullOrWhiteSpace(locationId))
            query = query.Where(ld => ld.LocationId == locationId);

        if (!string.IsNullOrWhiteSpace(departmentId))
            query = query.Where(ld => ld.DepartmentId == departmentId);

        return await query
            .OrderBy(ld => ld.Location != null ? ld.Location.LocationName : string.Empty)
            .ThenBy(ld => ld.Department != null ? ld.Department.DepartmentName : string.Empty)
            .ToListAsync(cancellationToken);
    }

    public async Task<LocationDepartment?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.LocationDepartments
            .Include(ld => ld.Location)
            .Include(ld => ld.Department)
            .FirstOrDefaultAsync(ld => ld.Id == id && ld.IsActive, cancellationToken);
    }

    public async Task<string?> GetLocationIdByDepartmentIdAsync(string departmentId, CancellationToken cancellationToken = default)
    {
        return await _context.LocationDepartments
            .Where(ld => ld.DepartmentId == departmentId)
            .Select(ld => ld.LocationId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<string?> GetLocationNameByDepartmentIdAsync(string departmentId, CancellationToken cancellationToken = default)
    {
        return await _context.LocationDepartments
            .Where(ld => ld.DepartmentId == departmentId)
            .Select(ld => ld.Location != null ? ld.Location.LocationName : null)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task SetLocationForDepartmentAsync(string departmentId, string locationId, CancellationToken cancellationToken = default)
    {
        var existingMappings = await _context.LocationDepartments
            .Where(ld => ld.DepartmentId == departmentId)
            .ToListAsync(cancellationToken);

        if (existingMappings.Count > 0)
            _context.LocationDepartments.RemoveRange(existingMappings);

        _context.LocationDepartments.Add(new LocationDepartment
        {
            DepartmentId = departmentId,
            LocationId = locationId,
            IsActive = true
        });

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<string> AddAsync(LocationDepartment locationDepartment, CancellationToken cancellationToken = default)
    {
        _context.LocationDepartments.Add(locationDepartment);
        await _context.SaveChangesAsync(cancellationToken);
        return locationDepartment.Id;
    }

    public async Task UpdateAsync(LocationDepartment locationDepartment, CancellationToken cancellationToken = default)
    {
        _context.LocationDepartments.Update(locationDepartment);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(LocationDepartment locationDepartment, CancellationToken cancellationToken = default)
    {
        _context.LocationDepartments.Remove(locationDepartment);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.LocationDepartments.AnyAsync(ld => ld.Id == id, cancellationToken);
    }
}
