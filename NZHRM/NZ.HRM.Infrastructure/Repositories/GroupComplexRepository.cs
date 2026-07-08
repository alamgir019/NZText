using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class GroupComplexRepository : IGroupComplexRepository
{
    private readonly ApplicationDbContext _context;

    public GroupComplexRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<MstGroupComplex>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.MstGroupComplexes.AsQueryable();
        if (!includeInactive) query = query.Where(g => g.IsActive);
        return await query.OrderBy(g => g.ComplexName).ToListAsync(cancellationToken);
    }

    public async Task<MstGroupComplex?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.MstGroupComplexes.FirstOrDefaultAsync(g => g.Id == id && g.IsActive, cancellationToken);
    }

    public async Task<string> AddAsync(MstGroupComplex groupComplex, CancellationToken cancellationToken = default)
    {
        _context.MstGroupComplexes.Add(groupComplex);
        await _context.SaveChangesAsync(cancellationToken);
        return groupComplex.Id;
    }

    public async Task UpdateAsync(MstGroupComplex groupComplex, CancellationToken cancellationToken = default)
    {
        _context.MstGroupComplexes.Update(groupComplex);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MstGroupComplex groupComplex, CancellationToken cancellationToken = default)
    {
        _context.MstGroupComplexes.Remove(groupComplex);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.MstGroupComplexes.AnyAsync(g => g.Id == id, cancellationToken);
    }
}
