using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class GroupRepository : IGroupRepository
{
    private readonly ApplicationDbContext _context;

    public GroupRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<MstGroup>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.MstGroups.AsQueryable();
        if (!includeInactive) query = query.Where(g => g.IsActive);
        return await query.OrderBy(g => g.GroupName).ToListAsync(cancellationToken);
    }

    public async Task<MstGroup?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.MstGroups.FirstOrDefaultAsync(g => g.Id == id && g.IsActive, cancellationToken);
    }

    public async Task<string> AddAsync(MstGroup group, CancellationToken cancellationToken = default)
    {
        _context.MstGroups.Add(group);
        await _context.SaveChangesAsync(cancellationToken);
        return group.Id;
    }

    public async Task UpdateAsync(MstGroup group, CancellationToken cancellationToken = default)
    {
        _context.MstGroups.Update(group);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MstGroup group, CancellationToken cancellationToken = default)
    {
        _context.MstGroups.Remove(group);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.MstGroups.AnyAsync(g => g.Id == id, cancellationToken);
    }
}
