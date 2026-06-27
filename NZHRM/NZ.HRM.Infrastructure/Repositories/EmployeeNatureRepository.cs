using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class EmployeeNatureRepository : IEmployeeNatureRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeNatureRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<LookEmployeeNature>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.EmployeeNatures.AsQueryable();

        if (!includeInactive)
            query = query.Where(x => x.IsActive);

        return await query
            .OrderBy(x => x.SortOrder)
            .ThenBy(x => x.NatureName)
            .ToListAsync(cancellationToken);
    }

    public async Task<LookEmployeeNature?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.EmployeeNatures
            .FirstOrDefaultAsync(x => x.Id == id && x.IsActive, cancellationToken);
    }

    public async Task<string> AddAsync(LookEmployeeNature employeeNature, CancellationToken cancellationToken = default)
    {
        _context.EmployeeNatures.Add(employeeNature);
        await _context.SaveChangesAsync(cancellationToken);
        return employeeNature.Id;
    }

    public async Task UpdateAsync(LookEmployeeNature employeeNature, CancellationToken cancellationToken = default)
    {
        _context.EmployeeNatures.Update(employeeNature);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(LookEmployeeNature employeeNature, CancellationToken cancellationToken = default)
    {
        _context.EmployeeNatures.Remove(employeeNature);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.EmployeeNatures.AnyAsync(x => x.Id == id, cancellationToken);
    }
}
