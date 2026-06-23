using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;
using NZ.HRM.Mapping.Employees;

namespace NZ.HRM.Infrastructure.Repositories;

public class EmployeeMasterRepository : IEmployeeMasterRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeMasterRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<HrmEmployeeMaster>> GetAllAsync(DateTime? onDate = null, bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.HrmEmployeeMasters
            .Include(e => e.Employment.Unit)
            .Include(e => e.Employment.Department)
            .Include(e => e.Employment.Section)
            .Include(e => e.Employment.Grade)
            .Include(e => e.Employment.Shift)
            //.Include(e => e.Employment.EmployeeNature)
            //.Include(e => e.Employment.Holiday)
            //.Include(e => e.VerificationInfo)
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

    public async Task<List<HrmEmployeeMaster>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.HrmEmployeeMasters
            .Include(e => e.Employment.Unit)
            .Include(e => e.Employment.Department)
            .Include(e => e.Employment.Section)
            .Include(e => e.Employment.Grade)
            .Include(e => e.Employment.Shift)
            //.Include(e => e.Employment.EmployeeNature)
            //.Include(e => e.Employment.Holiday)
            //.Include(e => e.VerificationInfo)
            .AsQueryable();
        if (!includeInactive)
        {
            query = query.Where(e => e.IsActive);
        }

        return await query
            .OrderBy(e => e.EmployeeCode)
            .ToListAsync(cancellationToken);
    }


    public async Task<List<HrmEmployeeMaster>> GetByDateAsync(DateTime onDate, bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.HrmEmployeeMasters
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

    public async Task<HrmEmployeeMaster?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.HrmEmployeeMasters
            .Include(e => e.Employment.Unit)
            .Include(e => e.Employment.Department)
            .Include(e => e.Employment.Section)
            .Include(e => e.Employment.Grade)
            //.Include(e => e.PersonalInfo)
            //.Include(e => e.VerificationInfo)
            .Include(e => e.Employment.Designation)
            .Include(e => e.Employment.Shift)
            //.Include(e => e.Employment.EmployeeNature)
            .FirstOrDefaultAsync(e => e.Id == id && e.IsActive, cancellationToken);
    }

    public async Task<HrmEmployeeMaster?> GetByEmployeeCodeAsync(string employeeCode, CancellationToken cancellationToken = default)
    {
        return await _context.HrmEmployeeMasters
            .Include(e => e.Employment.Unit)
            .Include(e => e.Employment.Department)
            .Include(e => e.Employment.Section)
            .Include(e => e.Employment.Grade)
            .Include(e => e.Employment.Shift)
            //.Include(e => e.Employment.EmployeeNature)
            //.Include(e => e.Employment.Holiday)
            .FirstOrDefaultAsync(e => e.EmployeeCode == employeeCode && e.IsActive, cancellationToken);
    }

    public async Task<List<HrmEmployeeMaster>> GetByCompanyIdAsync(string companyId, CancellationToken cancellationToken = default)
    {
        return await _context.HrmEmployeeMasters
            .Include(e => e.Employment.Unit)
            .Include(e => e.Employment.Department)
            .Include(e => e.Employment.Section)
            .Include(e => e.Employment.Grade)
            .Include(e => e.Employment.Shift)
            //.Include(e => e.Employment.EmployeeNature)
            //.Include(e => e.Employment.Holiday)
            .Where(e => e.Employment.UnitId == companyId && e.IsActive)
            .OrderBy(e => e.EmployeeCode)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<HrmEmployeeMaster>> GetByDepartmentIdAsync(string departmentId, CancellationToken cancellationToken = default)
    {
        return await _context.HrmEmployeeMasters
            .Include(e => e.Employment.Unit)
            .Include(e => e.Employment.Department)
            .Include(e => e.Employment.Section)
            .Include(e => e.Employment.Grade)
            .Include(e => e.Employment.Shift)
            //.Include(e => e.Employment.EmployeeNature)
            //.Include(e => e.Employment.Holiday)
            .Where(e => e.Employment.DepartmentId == departmentId && e.IsActive)
            .OrderBy(e => e.EmployeeCode)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<HrmEmployeeMaster>> GetByStatusAsync(string status, CancellationToken cancellationToken = default)
    {
        var normalizedStatus = status.Trim();

        if (string.IsNullOrWhiteSpace(normalizedStatus))
        {
            return new List<HrmEmployeeMaster>();
        }

        return await _context.HrmEmployeeMasters
            .Include(e => e.Personal)
            .Include(e => e.MedicalFitnessCheck)
            .Where(e => e.Status != null && EF.Functions.ILike(e.Status, normalizedStatus))
            .OrderBy(e => e.EnrollmentId)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<HrmEmployeeMaster>> SearchAsync(string searchText, CancellationToken cancellationToken = default)
    {
        var searchTerm = searchText.Trim();
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return new List<HrmEmployeeMaster>();
        }

        var searchPattern = $"%{searchTerm}%";

        return await _context.HrmEmployeeMasters
            //.Include(e => e.PersonalInfo)
            .Where(e => e.IsActive &&
                       (EF.Functions.Like(e.EnrollmentId ?? string.Empty, searchPattern)
                        || EF.Functions.ILike(e.EmployeeNameEnglish, searchPattern)
                        || (e.EmployeeNameBangla != null) && EF.Functions.ILike(e.EmployeeNameBangla, searchPattern)
                        //|| (e.PersonalInfo != null && EF.Functions.Like(e.PersonalInfo.MobileNumber, searchPattern))
                        ))
            .OrderBy(e => e.EmployeeNameEnglish)
            .ToListAsync(cancellationToken);
    }

    public async Task<string> AddAsync(HrmEmployeeMaster employeeMaster, CancellationToken cancellationToken = default)
    {
        _context.HrmEmployeeMasters.Add(employeeMaster);
        await _context.SaveChangesAsync(cancellationToken);
        return employeeMaster.Id;
    }

    public async Task UpdateAsync(HrmEmployeeMaster employeeMaster, CancellationToken cancellationToken = default)
    {
        _context.HrmEmployeeMasters.Update(employeeMaster);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(HrmEmployeeMaster employeeMaster, CancellationToken cancellationToken = default)
    {
        _context.HrmEmployeeMasters.Remove(employeeMaster);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.HrmEmployeeMasters
            .AnyAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<bool> EmployeeCodeExistsAsync(string employeeCode, CancellationToken cancellationToken = default)
    {
        return await _context.HrmEmployeeMasters
            .AnyAsync(e => e.EmployeeCode == employeeCode, cancellationToken);
    }

    public async Task<bool> EnrollmentCodeExistsAsync(string enrollmentCode , CancellationToken cancellationToken = default)
    {
        return await _context.HrmEmployeeMasters
            .AnyAsync(e => e.EnrollmentId == enrollmentCode, cancellationToken);
    }

    public async Task UpdateRangeAsync(List<HrmEmployeeMaster> employeeMasters, CancellationToken cancellationToken = default)
    {
        _context.HrmEmployeeMasters.UpdateRange(employeeMasters);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
