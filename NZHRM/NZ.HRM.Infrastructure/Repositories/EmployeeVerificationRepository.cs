using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class EmployeeVerificationRepository : IEmployeeVerificationRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeVerificationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EmployeeVerification?> GetByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken = default)
    {
        return await _context.EmployeeVerifications
            .Include(ev => ev.Employee)
            .FirstOrDefaultAsync(ev => ev.EmployeeId == employeeId, cancellationToken);
    }

    public async Task<string> AddAsync(EmployeeVerification employeeVerification, CancellationToken cancellationToken = default)
    {
        _context.EmployeeVerifications.Add(employeeVerification);
        await _context.SaveChangesAsync(cancellationToken);
        return employeeVerification.Id;
    }

    public async Task UpdateAsync(EmployeeVerification employeeVerification, CancellationToken cancellationToken = default)
    {
        _context.EmployeeVerifications.Update(employeeVerification);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
