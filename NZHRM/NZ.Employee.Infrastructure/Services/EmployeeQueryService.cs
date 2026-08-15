using Microsoft.EntityFrameworkCore;
using NZ.Employee.Infrastructure.Persistence;
using NZ.Shared.Contracts.HRM;

namespace NZ.Employee.Infrastructure.Services;

/// <summary>
/// IEmployeeQuery implementation for the Employee module.
/// Used by other modules (Payroll, Attendance) via the shared contracts.
/// </summary>
public class EmployeeQueryService : IEmployeeQuery
{
    private readonly EmployeeDbContext _context;

    public EmployeeQueryService(EmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<EmployeeInfo?> GetByIdAsync(string employeeId, CancellationToken cancellationToken = default)
    {
        var employee = await _context.HrmEmployeeMasters
            .Include(e => e.Employment)
            .FirstOrDefaultAsync(e => e.Id == employeeId && e.IsActive, cancellationToken);

        if (employee == null) return null;

        return new EmployeeInfo(
            employee.Id,
            employee.EmployeeCode,
            employee.EmployeeName,
            employee.Employment?.DesignationId ?? string.Empty,
            employee.Employment?.GradeId ?? string.Empty,
            employee.Employment?.DepartmentId ?? string.Empty,
            employee.Employment?.ProcessingGroupId ?? string.Empty);
    }

    public async Task<IReadOnlyList<EmployeeInfo>> GetActiveByUnitAsync(string unitCode, CancellationToken cancellationToken = default)
    {
        var employees = await _context.HrmEmployeeMasters
            .Include(e => e.Employment)
            .Where(e => e.IsActive && e.Employment != null && e.Employment.UnitId == unitCode)
            .ToListAsync(cancellationToken);

        return employees.Select(e => new EmployeeInfo(
            e.Id,
            e.EmployeeCode,
            e.EmployeeName,
            e.Employment?.DesignationId ?? string.Empty,
            e.Employment?.GradeId ?? string.Empty,
            e.Employment?.DepartmentId ?? string.Empty,
            e.Employment?.ProcessingGroupId ?? string.Empty)).ToList();
    }
}
