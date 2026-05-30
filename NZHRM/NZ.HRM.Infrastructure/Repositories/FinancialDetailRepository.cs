using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class FinancialDetailRepository : IFinancialDetailRepository
{
    private readonly ApplicationDbContext _context;

    public FinancialDetailRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<FinancialDetail>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.Set<FinancialDetail>().AsQueryable();

        if (!includeInactive)
            query = query.Where(x => x.IsActive);

        return await query
            .OrderByDescending(x => x.UpdatedOn)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<FinancialDetail>> GetByEmployeeIdAsync(string employeeId, bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.Set<FinancialDetail>()
            .Where(x => x.EmployeeId == employeeId)
            .AsQueryable();

        if (!includeInactive)
            query = query.Where(x => x.IsActive);

        return await query
            .OrderByDescending(x => x.UpdatedOn)
            .ToListAsync(cancellationToken);
    }

    public async Task<FinancialDetail?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<FinancialDetail>()
            .FirstOrDefaultAsync(x => x.Id == id && x.IsActive, cancellationToken);
    }

    public async Task<string> AddAsync(FinancialDetail financialDetail, CancellationToken cancellationToken = default)
    {
        _context.Set<FinancialDetail>().Add(financialDetail);
        await _context.SaveChangesAsync(cancellationToken);
        return financialDetail.Id;
    }

    public async Task UpdateAsync(FinancialDetail financialDetail, CancellationToken cancellationToken = default)
    {
        _context.Set<FinancialDetail>().Update(financialDetail);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(FinancialDetail financialDetail, CancellationToken cancellationToken = default)
    {
        _context.Set<FinancialDetail>().Remove(financialDetail);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<FinancialDetail>().AnyAsync(x => x.Id == id, cancellationToken);
    }
}
