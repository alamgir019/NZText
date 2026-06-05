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

    public async Task<List<HrmEmployeePersonal>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.HrmEmployeePersonals
            .Include(ep => ep.Employee)
            .ToListAsync(cancellationToken);
    }

    public async Task<HrmEmployeePersonal?> GetByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken = default)
    {
        return await _context.HrmEmployeePersonals
            .Include(ep => ep.Employee)
            .FirstOrDefaultAsync(ep => ep.EmployeeId == employeeId, cancellationToken);
    }

    public async Task<HrmEmployeePersonal?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.HrmEmployeePersonals
            .Include(ep => ep.Employee)
            .FirstOrDefaultAsync(ep => ep.Id == id, cancellationToken);
    }

    public async Task<string> AddAsync(HrmEmployeePersonal employeePersonal, CancellationToken cancellationToken = default)
    {
        _context.HrmEmployeePersonals.Add(employeePersonal);
        await _context.SaveChangesAsync(cancellationToken);
        return employeePersonal.Id;
    }

    public async Task UpdateAsync(HrmEmployeePersonal employeePersonal, CancellationToken cancellationToken = default)
    {
        _context.HrmEmployeePersonals.Update(employeePersonal);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(HrmEmployeePersonal employeePersonal, CancellationToken cancellationToken = default)
    {
        _context.HrmEmployeePersonals.Remove(employeePersonal);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsForEmployeeAsync(string employeeId, CancellationToken cancellationToken = default)
    {
        return await _context.HrmEmployeePersonals
            .AnyAsync(ep => ep.EmployeeId == employeeId, cancellationToken);
    }
}
