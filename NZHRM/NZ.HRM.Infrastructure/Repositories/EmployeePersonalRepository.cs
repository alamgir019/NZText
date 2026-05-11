using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class EmployeePersonalRepository : IEmployeePersonalRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeePersonalRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<EmployeePersonal>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.EmployeePersonals
            .Include(ep => ep.Employee)
            .ToListAsync(cancellationToken);
    }

    public async Task<EmployeePersonal?> GetByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken = default)
    {
        return await _context.EmployeePersonals
            .Include(ep => ep.Employee)
            .FirstOrDefaultAsync(ep => ep.EmployeeId == employeeId, cancellationToken);
    }

    public async Task<EmployeePersonal?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.EmployeePersonals
            .Include(ep => ep.Employee)
            .FirstOrDefaultAsync(ep => ep.Id == id, cancellationToken);
    }

    public async Task<string> AddAsync(EmployeePersonal employeePersonal, CancellationToken cancellationToken = default)
    {
        _context.EmployeePersonals.Add(employeePersonal);
        await _context.SaveChangesAsync(cancellationToken);
        return employeePersonal.Id;
    }

    public async Task UpdateAsync(EmployeePersonal employeePersonal, CancellationToken cancellationToken = default)
    {
        _context.EmployeePersonals.Update(employeePersonal);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(EmployeePersonal employeePersonal, CancellationToken cancellationToken = default)
    {
        _context.EmployeePersonals.Remove(employeePersonal);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsForEmployeeAsync(string employeeId, CancellationToken cancellationToken = default)
    {
        return await _context.EmployeePersonals
            .AnyAsync(ep => ep.EmployeeId == employeeId, cancellationToken);
    }
}
