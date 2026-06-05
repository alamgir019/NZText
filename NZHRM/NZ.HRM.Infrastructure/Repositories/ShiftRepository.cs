using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class ShiftRepository : IShiftRepository
{
    private readonly ApplicationDbContext _context;

    public ShiftRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<MstShift>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.MstShifts.AsQueryable();

        if (!includeInactive)
            query = query.Where(s => s.IsActive);

        return await query
            .OrderBy(s => s.SortOrder)
            .ThenBy(s => s.ShiftName)
            .ToListAsync(cancellationToken);
    }

    public async Task<MstShift?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.MstShifts
            .FirstOrDefaultAsync(s => s.Id == id && s.IsActive, cancellationToken);
    }

    public async Task<string> AddAsync(MstShift shift, CancellationToken cancellationToken = default)
    {
        _context.MstShifts.Add(shift);
        await _context.SaveChangesAsync(cancellationToken);
        return shift.Id;
    }

    public async Task UpdateAsync(MstShift shift, CancellationToken cancellationToken = default)
    {
        _context.MstShifts.Update(shift);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MstShift shift, CancellationToken cancellationToken = default)
    {
        _context.MstShifts.Remove(shift);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.MstShifts
            .AnyAsync(s => s.Id == id, cancellationToken);
    }
}
