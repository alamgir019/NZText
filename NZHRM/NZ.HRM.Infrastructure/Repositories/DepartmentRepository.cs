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

    public async Task<List<Department>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.Departments.AsQueryable();

        if (!includeInactive)
        {
            query = query.Where(d => d.IsActive);
        }

        return await query
            .OrderBy(d => d.DepartmentName)
            .ToListAsync(cancellationToken);
    }

    public async Task<Department?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Departments
            .FirstOrDefaultAsync(d => d.Id == id && d.IsActive, cancellationToken);
    }

    public async Task<string> AddAsync(Department department, CancellationToken cancellationToken = default)
    {
        _context.Departments.Add(department);
        await _context.SaveChangesAsync(cancellationToken);
        return department.Id;
    }

    public async Task UpdateAsync(Department department, CancellationToken cancellationToken = default)
    {
        _context.Departments.Update(department);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Department department, CancellationToken cancellationToken = default)
    {
        _context.Departments.Remove(department);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Departments
            .AnyAsync(d => d.Id == id, cancellationToken);
    }
}
