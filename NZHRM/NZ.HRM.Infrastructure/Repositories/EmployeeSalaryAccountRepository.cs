using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class EmployeeSalaryAccountRepository : IEmployeeSalaryAccountRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeSalaryAccountRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<HrmEmployeeSalaryAccount>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.Set<HrmEmployeeSalaryAccount>().AsQueryable();

        if (!includeInactive)
            query = query.Where(x => x.IsActive);

        return await query
            .OrderByDescending(x => x.UpdatedOn)
            .ToListAsync(cancellationToken);
    }

    public async Task<HrmEmployeeSalaryAccount?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<HrmEmployeeSalaryAccount>()
            .FirstOrDefaultAsync(x => x.Id == id && x.IsActive, cancellationToken);
    }

    public async Task<HrmEmployeeSalaryAccount?> GetByEmployeeIdAsync(string employeeId, bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.Set<HrmEmployeeSalaryAccount>()
            .Where(x => x.EmployeeId == employeeId)
            .AsQueryable();

        if (!includeInactive)
            query = query.Where(x => x.IsActive);

        return await query
            .OrderByDescending(x => x.UpdatedOn)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<string> AddAsync(HrmEmployeeSalaryAccount salaryAccount, CancellationToken cancellationToken = default)
    {
        _context.Set<HrmEmployeeSalaryAccount>().Add(salaryAccount);
        await _context.SaveChangesAsync(cancellationToken);
        return salaryAccount.Id;
    }

    public async Task UpdateAsync(HrmEmployeeSalaryAccount salaryAccount, CancellationToken cancellationToken = default)
    {
        _context.Set<HrmEmployeeSalaryAccount>().Update(salaryAccount);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(HrmEmployeeSalaryAccount salaryAccount, CancellationToken cancellationToken = default)
    {
        _context.Set<HrmEmployeeSalaryAccount>().Remove(salaryAccount);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<HrmEmployeeSalaryAccount>().AnyAsync(x => x.Id == id, cancellationToken);
    }
}