using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class RolePermissionRepository : IRolePermissionRepository
{
    private readonly ApplicationDbContext _context;

    public RolePermissionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<SecRolePermission>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SecRolePermissions
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<SecRolePermission?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.SecRolePermissions
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<string> AddAsync(SecRolePermission secRolePermission, CancellationToken cancellationToken = default)
    {
        _context.SecRolePermissions.Add(secRolePermission);
        await _context.SaveChangesAsync(cancellationToken);
        return secRolePermission.Id;
    }

    public async Task UpdateAsync(SecRolePermission secRolePermission, CancellationToken cancellationToken = default)
    {
        _context.SecRolePermissions.Update(secRolePermission);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(SecRolePermission secRolePermission, CancellationToken cancellationToken = default)
    {
        _context.SecRolePermissions.Remove(secRolePermission);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.SecRolePermissions.AnyAsync(x => x.Id == id, cancellationToken);
    }
}
