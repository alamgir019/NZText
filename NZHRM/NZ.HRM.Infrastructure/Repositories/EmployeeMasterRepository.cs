using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class EmployeeMasterRepository : IEmployeeMasterRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeMasterRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<EmployeeMaster>> GetAllAsync(DateTime? onDate = null, bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.EmployeeMasters
            .Include(e => e.Company)
            .Include(e => e.Department)
            .Include(e => e.Section)
            .Include(e => e.Grade)
            .Include(e => e.Shift)
            .Include(e => e.Holiday)
            .Include(e => e.VerificationInfo)
            .AsQueryable();
        if (!includeInactive)
        {
            query = query.Where(e => e.IsActive);
        }

        if (onDate.HasValue)
        {
            var d = onDate.Value.Date;
            query = query.Where(e => e.CreatedOn.ToUniversalTime().Date == d);
        }

        return await query
            .OrderBy(e => e.EmployeeCode)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<EmployeeMaster>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.EmployeeMasters
            .Include(e => e.Company)
            .Include(e => e.Department)
            .Include(e => e.Section)
            .Include(e => e.Grade)
            .Include(e => e.Shift)
            .Include(e => e.Holiday)
            .Include(e => e.VerificationInfo)
            .AsQueryable();
        if (!includeInactive)
        {
            query = query.Where(e => e.IsActive);
        }

        return await query
            .OrderBy(e => e.EmployeeCode)
            .ToListAsync(cancellationToken);
    }


    public async Task<List<EmployeeMaster>> GetByDateAsync(DateTime onDate, bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.EmployeeMasters
            .AsQueryable();
        if (!includeInactive)
        {
            query = query.Where(e => e.IsActive);
        }

        query = query.Where(e => e.CreatedOn.ToUniversalTime().Date == onDate.Date);

        return await query
            .OrderBy(e => e.EnrollmentId)
            .ToListAsync(cancellationToken);
    }

    public async Task<EmployeeMaster?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.EmployeeMasters
            .Include(e => e.Company)
            .Include(e => e.Department)
            .Include(e => e.Section)
            .Include(e => e.Grade)
            .Include(e => e.PersonalInfo)
            .Include(e => e.VerificationInfo)
            .FirstOrDefaultAsync(e => e.Id == id && e.IsActive, cancellationToken);
    }

    public async Task<EmployeeMaster?> GetByEmployeeCodeAsync(string employeeCode, CancellationToken cancellationToken = default)
    {
        return await _context.EmployeeMasters
            .Include(e => e.Company)
            .Include(e => e.Department)
            .Include(e => e.Section)
            .Include(e => e.Grade)
            .Include(e => e.Shift)
            .Include(e => e.Holiday)
            .FirstOrDefaultAsync(e => e.EmployeeCode == employeeCode && e.IsActive, cancellationToken);
    }

    public async Task<List<EmployeeMaster>> GetByCompanyIdAsync(string companyId, CancellationToken cancellationToken = default)
    {
        return await _context.EmployeeMasters
            .Include(e => e.Department)
            .Include(e => e.Section)
            .Include(e => e.Grade)
            .Include(e => e.Shift)
            .Include(e => e.Holiday)
            .Where(e => e.CompanyId == companyId && e.IsActive)
            .OrderBy(e => e.EmployeeCode)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<EmployeeMaster>> GetByDepartmentIdAsync(string departmentId, CancellationToken cancellationToken = default)
    {
        return await _context.EmployeeMasters
            .Include(e => e.Company)
            .Include(e => e.Section)
            .Include(e => e.Grade)
            .Include(e => e.Shift)
            .Include(e => e.Holiday)
            .Where(e => e.DepartmentId == departmentId && e.IsActive)
            .OrderBy(e => e.EmployeeCode)
            .ToListAsync(cancellationToken);
    }

    public async Task<string> AddAsync(EmployeeMaster employeeMaster, CancellationToken cancellationToken = default)
    {
        _context.EmployeeMasters.Add(employeeMaster);
        await _context.SaveChangesAsync(cancellationToken);
        return employeeMaster.Id;
    }

    public async Task UpdateAsync(EmployeeMaster employeeMaster, CancellationToken cancellationToken = default)
    {
        _context.EmployeeMasters.Update(employeeMaster);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(EmployeeMaster employeeMaster, CancellationToken cancellationToken = default)
    {
        _context.EmployeeMasters.Remove(employeeMaster);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.EmployeeMasters
            .AnyAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<bool> EmployeeCodeExistsAsync(string employeeCode, CancellationToken cancellationToken = default)
    {
        return await _context.EmployeeMasters
            .AnyAsync(e => e.EmployeeCode == employeeCode, cancellationToken);
    }

    public async Task<bool> EnrollmentCodeExistsAsync(string enrollmentCode , CancellationToken cancellationToken = default)
    {
        return await _context.EmployeeMasters
            .AnyAsync(e => e.EnrollmentId == enrollmentCode, cancellationToken);
    }
}
