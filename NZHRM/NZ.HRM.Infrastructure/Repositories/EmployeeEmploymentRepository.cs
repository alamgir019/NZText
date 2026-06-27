using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class EmployeeEmploymentRepository : IEmployeeEmploymentRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeEmploymentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<HrmEmployeeEmployment>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.HrmEmployeeEmployments
            .Include(ee => ee.Employee)
            .Include(ee => ee.Group)
            .Include(ee => ee.Unit)
            .Include(ee => ee.Subunit)
            .Include(ee => ee.Department)
            .Include(ee => ee.Section)
            .Include(ee => ee.Cell)
            .Include(ee => ee.Designation)
            .Include(ee => ee.Grade)
            .Include(ee => ee.Shift)
            .Include(ee => ee.EmployeeCategory)
            .Include(ee => ee.ReportingEmployee)
            .Include(ee => ee.ProcessingGroup)
            .Include(ee => ee.EmployeeNature)
            .ToListAsync(cancellationToken);
    }

    public async Task<HrmEmployeeEmployment?> GetByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken = default)
    {
        return await _context.HrmEmployeeEmployments
            .Include(ee => ee.Employee)
            .Include(ee => ee.Department)
            .Include(ee => ee.Section)
            .Include(ee => ee.Designation)
            .Include(ee => ee.Grade)
            .Include(ee => ee.Shift)
            .FirstOrDefaultAsync(ee => ee.EmployeeId == employeeId, cancellationToken);
    }

    public async Task<HrmEmployeeEmployment?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.HrmEmployeeEmployments
            .Include(ee => ee.Employee)
            .Include(ee => ee.Department)
            .Include(ee => ee.Section)
            .Include(ee => ee.Designation)
            .Include(ee => ee.Grade)
            .Include(ee => ee.Shift)
            .FirstOrDefaultAsync(ee => ee.Id == id, cancellationToken);
    }

    public async Task<string> AddAsync(HrmEmployeeEmployment employeeEmployment, CancellationToken cancellationToken = default)
    {
        _context.HrmEmployeeEmployments.Add(employeeEmployment);
        await _context.SaveChangesAsync(cancellationToken);
        return employeeEmployment.Id;
    }

    public async Task UpdateAsync(HrmEmployeeEmployment employeeEmployment, CancellationToken cancellationToken = default)
    {
        _context.HrmEmployeeEmployments.Update(employeeEmployment);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(HrmEmployeeEmployment employeeEmployment, CancellationToken cancellationToken = default)
    {
        _context.HrmEmployeeEmployments.Remove(employeeEmployment);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsForEmployeeAsync(string employeeId, CancellationToken cancellationToken = default)
    {
        return await _context.HrmEmployeeEmployments
            .AnyAsync(ee => ee.EmployeeId == employeeId, cancellationToken);
    }
}