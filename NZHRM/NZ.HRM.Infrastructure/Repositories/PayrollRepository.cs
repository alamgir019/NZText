using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class PayrollRepository : IPayrollRepository
{
    private readonly ApplicationDbContext _context;

    public PayrollRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<HrmEmployeePayroll>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.Set<HrmEmployeePayroll>().AsQueryable();

        if (!includeInactive)
            query = query.Where(x => x.IsActive);

        return await query
            .OrderByDescending(x => x.UpdatedOn)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<HrmEmployeePayroll>> GetByEmployeeIdAsync(string employeeId, bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.Set<HrmEmployeePayroll>()
            .Where(x => x.EmployeeId == employeeId)
            .AsQueryable();

        if (!includeInactive)
            query = query.Where(x => x.IsActive);

        return await query
            .OrderByDescending(x => x.UpdatedOn)
            .ToListAsync(cancellationToken);
    }

    public async Task<HrmEmployeePayroll?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<HrmEmployeePayroll>()
            .FirstOrDefaultAsync(x => x.Id == id && x.IsActive, cancellationToken);
    }

    public async Task<string> AddAsync(HrmEmployeePayroll financialDetail, CancellationToken cancellationToken = default)
    {
        _context.Set<HrmEmployeePayroll>().Add(financialDetail);
        await _context.SaveChangesAsync(cancellationToken);
        return financialDetail.Id;
    }

    public async Task UpdateAsync(HrmEmployeePayroll financialDetail, CancellationToken cancellationToken = default)
    {
        _context.Set<HrmEmployeePayroll>().Update(financialDetail);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(HrmEmployeePayroll financialDetail, CancellationToken cancellationToken = default)
    {
        _context.Set<HrmEmployeePayroll>().Remove(financialDetail);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<HrmEmployeePayroll>().AnyAsync(x => x.Id == id, cancellationToken);
    }
}
