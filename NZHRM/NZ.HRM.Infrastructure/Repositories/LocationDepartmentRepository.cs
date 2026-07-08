using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class ComplexUnitDepartmentRepository : IComplexUnitDepartmentRepository
{
    private readonly ApplicationDbContext _context;

    public ComplexUnitDepartmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<MstDepartmentUnitComplex>> GetAllAsync(bool includeInactive = false, string? complexId = null, string? unitId = null, CancellationToken cancellationToken = default)
    {
        var query = _context.MstDepartmentUnitComplexes
            .Include(ld => ld.Complex)
            .Include(ld => ld.Department)
            .AsQueryable();

        if (!includeInactive)
            query = query.Where(ld => ld.IsActive);

        if (!string.IsNullOrWhiteSpace(complexId))
            query = query.Where(ld => ld.ComplexId == complexId);

        if (!string.IsNullOrWhiteSpace(unitId))
            query = query.Where(ld => ld.UnitId == unitId);

        return await query
            .OrderBy(ld => ld.Complex != null ? ld.Complex.ComplexName : string.Empty)
            .ThenBy(ld => ld.Unit != null ? ld.Unit.UnitName : string.Empty)
            .ThenBy(ld => ld.Department != null ? ld.Department.DepartmentName : string.Empty)
            .ToListAsync(cancellationToken);
    }

    public async Task<MstDepartmentUnitComplex?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.MstDepartmentUnitComplexes
            .Include(ld => ld.Complex)
            .Include(ld => ld.Department)
            .FirstOrDefaultAsync(ld => ld.Id == id && ld.IsActive, cancellationToken);
    }

    public async Task<string?> GetComplexIdByDepartmentIdAsync(string departmentId, CancellationToken cancellationToken = default)
    {
        return await _context.MstDepartmentUnitComplexes
            .Where(ld => ld.DepartmentId == departmentId)
            .Select(ld => ld.ComplexId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<string?> GetComplexNameByDepartmentIdAsync(string departmentId, CancellationToken cancellationToken = default)
    {
        return await _context.MstDepartmentUnitComplexes
            .Where(ld => ld.DepartmentId == departmentId)
            .Select(ld => ld.Complex != null ? ld.Complex.ComplexName : null)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task SetComplexForDepartmentAsync(string departmentId, string complexId, CancellationToken cancellationToken = default)
    {
        var existingMappings = await _context.MstDepartmentUnitComplexes
            .Where(ld => ld.DepartmentId == departmentId)
            .ToListAsync(cancellationToken);

        if (existingMappings.Count > 0)
            _context.MstDepartmentUnitComplexes.RemoveRange(existingMappings);

        _context.MstDepartmentUnitComplexes.Add(new MstDepartmentUnitComplex
        {
            DepartmentId = departmentId,
            ComplexId = complexId,
            IsActive = true
        });

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<string> AddAsync(MstDepartmentUnitComplex locationDepartment, CancellationToken cancellationToken = default)
    {
        _context.MstDepartmentUnitComplexes.Add(locationDepartment);
        await _context.SaveChangesAsync(cancellationToken);
        return locationDepartment.Id;
    }

    public async Task UpdateAsync(MstDepartmentUnitComplex locationDepartment, CancellationToken cancellationToken = default)
    {
        _context.MstDepartmentUnitComplexes.Update(locationDepartment);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MstDepartmentUnitComplex locationDepartment, CancellationToken cancellationToken = default)
    {
        _context.MstDepartmentUnitComplexes.Remove(locationDepartment);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.MstDepartmentUnitComplexes.AnyAsync(ld => ld.Id == id, cancellationToken);
    }
}
