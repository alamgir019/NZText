using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class UserRoleRepository : IUserRoleRepository
{
    private readonly ApplicationDbContext _context;

    public UserRoleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<SecUserRole>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SecUserRoles
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<SecUserRole?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.SecUserRoles
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<string> AddAsync(SecUserRole secUserRole, CancellationToken cancellationToken = default)
    {
        _context.SecUserRoles.Add(secUserRole);
        await _context.SaveChangesAsync(cancellationToken);
        return secUserRole.Id;
    }

    public async Task UpdateAsync(SecUserRole secUserRole, CancellationToken cancellationToken = default)
    {
        _context.SecUserRoles.Update(secUserRole);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(SecUserRole secUserRole, CancellationToken cancellationToken = default)
    {
        _context.SecUserRoles.Remove(secUserRole);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.SecUserRoles.AnyAsync(x => x.Id == id, cancellationToken);
    }
}
