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

    public async Task<List<Shift>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.Shifts.AsQueryable();

        if (!includeInactive)
            query = query.Where(s => s.IsActive);

        return await query
            .OrderBy(s => s.SortOrder)
            .ThenBy(s => s.ShiftName)
            .ToListAsync(cancellationToken);
    }

    public async Task<Shift?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Shifts
            .FirstOrDefaultAsync(s => s.Id == id && s.IsActive, cancellationToken);
    }

    public async Task<string> AddAsync(Shift shift, CancellationToken cancellationToken = default)
    {
        _context.Shifts.Add(shift);
        await _context.SaveChangesAsync(cancellationToken);
        return shift.Id;
    }

    public async Task UpdateAsync(Shift shift, CancellationToken cancellationToken = default)
    {
        _context.Shifts.Update(shift);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Shift shift, CancellationToken cancellationToken = default)
    {
        _context.Shifts.Remove(shift);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Shifts
            .AnyAsync(s => s.Id == id, cancellationToken);
    }
}
