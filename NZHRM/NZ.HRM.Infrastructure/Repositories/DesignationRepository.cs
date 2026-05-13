using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class DesignationRepository : IDesignationRepository
{
    private readonly ApplicationDbContext _context;

    public DesignationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Designation>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.Designations.AsQueryable();

        if (!includeInactive)
            query = query.Where(d => d.IsActive);

        return await query.OrderBy(d => d.DesignationName).ToListAsync(cancellationToken);
    }

    public async Task<Designation?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Designations.FirstOrDefaultAsync(d => d.Id == id && d.IsActive, cancellationToken);
    }

    public async Task<string> AddAsync(Designation designation, CancellationToken cancellationToken = default)
    {
        _context.Designations.Add(designation);
        await _context.SaveChangesAsync(cancellationToken);
        return designation.Id;
    }

    public async Task UpdateAsync(Designation designation, CancellationToken cancellationToken = default)
    {
        _context.Designations.Update(designation);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Designation designation, CancellationToken cancellationToken = default)
    {
        _context.Designations.Remove(designation);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Designations.AnyAsync(d => d.Id == id, cancellationToken);
    }
}
