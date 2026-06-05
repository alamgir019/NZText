using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly ApplicationDbContext _context;

    public CompanyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<MstUnit>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.MstUnits.AsQueryable();

        if (!includeInactive)
        {
            query = query.Where(c => c.IsActive);
        }

        return await query
            .OrderBy(c => c.UnitName)
            .ToListAsync(cancellationToken);
    }

    public async Task<MstUnit?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.MstUnits
            .FirstOrDefaultAsync(c => c.Id == id && c.IsActive, cancellationToken);
    }

    public async Task<string> AddAsync(MstUnit unit, CancellationToken cancellationToken = default)
    {
        _context.MstUnits.Add(unit);
        await _context.SaveChangesAsync(cancellationToken);
        return unit.Id;
    }

    public async Task UpdateAsync(MstUnit unit, CancellationToken cancellationToken = default)
    {
        _context.MstUnits.Update(unit);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MstUnit unit, CancellationToken cancellationToken = default)
    {
        _context.MstUnits.Remove(unit);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.MstUnits
            .AnyAsync(c => c.Id == id, cancellationToken);
    }
}