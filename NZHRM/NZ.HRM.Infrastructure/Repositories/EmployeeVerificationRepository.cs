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

    public async Task<HrmEmployeeVerification?> GetByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken = default)
    {
        return await _context.HrmEmployeeVerifications
            .Include(ev => ev.Employee)
            .FirstOrDefaultAsync(ev => ev.EmployeeId == employeeId, cancellationToken);
    }

    public async Task<string> AddAsync(HrmEmployeeVerification employeeVerification, CancellationToken cancellationToken = default)
    {
        _context.HrmEmployeeVerifications.Add(employeeVerification);
        await _context.SaveChangesAsync(cancellationToken);
        return employeeVerification.Id;
    }

    public async Task UpdateAsync(HrmEmployeeVerification employeeVerification, CancellationToken cancellationToken = default)
    {
        _context.HrmEmployeeVerifications.Update(employeeVerification);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
