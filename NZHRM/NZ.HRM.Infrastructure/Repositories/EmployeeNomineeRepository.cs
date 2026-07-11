using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class EmployeeNomineeRepository : IEmployeeNomineeRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeNomineeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<HrmEmployeeNominee>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.Set<HrmEmployeeNominee>().AsQueryable();

        if (!includeInactive)
            query = query.Where(x => x.IsActive);

        return await query
            .OrderByDescending(x => x.UpdatedOn)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<HrmEmployeeNominee>> GetByEmployeeIdAsync(string employeeId, bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.Set<HrmEmployeeNominee>()
            .Where(x => x.EmployeeId == employeeId)
            .AsQueryable();

        if (!includeInactive)
            query = query.Where(x => x.IsActive);

        return await query
            .OrderByDescending(x => x.UpdatedOn)
            .ToListAsync(cancellationToken);
    }

    public async Task<HrmEmployeeNominee?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<HrmEmployeeNominee>()
            .FirstOrDefaultAsync(x => x.Id == id && x.IsActive, cancellationToken);
    }

    public async Task<string> AddAsync(HrmEmployeeNominee employeeNominee, CancellationToken cancellationToken = default)
    {
        _context.Set<HrmEmployeeNominee>().Add(employeeNominee);
        await _context.SaveChangesAsync(cancellationToken);
        return employeeNominee.Id;
    }

    public async Task<IEnumerable<string>> UpdateRangeAsync(List<HrmEmployeeNominee> employeeNominees, CancellationToken cancellationToken = default)
    {
        _context.Set<HrmEmployeeNominee>().UpdateRange(employeeNominees);
        await _context.SaveChangesAsync(cancellationToken);
        return employeeNominees.Select(x => x.Id);
    }

    public async Task UpdateAsync(HrmEmployeeNominee employeeNominee, CancellationToken cancellationToken = default)
    {
        _context.Set<HrmEmployeeNominee>().Update(employeeNominee);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(HrmEmployeeNominee employeeNominee, CancellationToken cancellationToken = default)
    {
        _context.Set<HrmEmployeeNominee>().Remove(employeeNominee);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<HrmEmployeeNominee>().AnyAsync(x => x.Id == id, cancellationToken);
    }
}
