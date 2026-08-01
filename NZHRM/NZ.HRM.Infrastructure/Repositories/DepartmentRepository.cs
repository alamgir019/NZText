using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly ApplicationDbContext _context;

    public DepartmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<MstDepartment>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.MstDepartments.AsQueryable();

        if (!includeInactive)
        {
            query = query.Where(d => d.IsActive);
        }

        return await query
            .OrderBy(d => d.SortOrder)
            .ThenBy(d => d.DepartmentName)
            .ToListAsync(cancellationToken);
    }

    public async Task<MstDepartment?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.MstDepartments
            .FirstOrDefaultAsync(d => d.Id == id && d.IsActive, cancellationToken);
    }

    public async Task<string> AddAsync(MstDepartment department, CancellationToken cancellationToken = default)
    {
        _context.MstDepartments.Add(department);
        await _context.SaveChangesAsync(cancellationToken);
        return department.Id;
    }

    public async Task UpdateAsync(MstDepartment department, CancellationToken cancellationToken = default)
    {
        _context.MstDepartments.Update(department);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MstDepartment department, CancellationToken cancellationToken = default)
    {
        _context.MstDepartments.Remove(department);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.MstDepartments
            .AnyAsync(d => d.Id == id, cancellationToken);
    }
}
