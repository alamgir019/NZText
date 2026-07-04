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

    public async Task<List<HrmEmployeeMaster>> GetByStatusUpToDateAsync(string status, DateTime upToUtc, bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var normalizedStatus = status?.Trim();

        if (string.IsNullOrWhiteSpace(normalizedStatus))
            return new List<HrmEmployeeMaster>();

        var query = _context.HrmEmployeeMasters
            .Include(e => e.Personal)
            .Include(e => e.MedicalFitnessCheck)
            .Include(e => e.Employment.Unit)
            .Include(e => e.Employment.Department)
            .Include(e => e.Employment.Section)
            .Include(e => e.Employment.Cell)
            .Include(e => e.Employment.Grade)
            .Include(e => e.Employment.Designation)
            .Include(e => e.Employment.Shift)
            .Include(e => e.Payroll).ThenInclude(p => p.SalaryAccount)
            .AsQueryable();

        if (!includeInactive)
        {
            query = query.Where(e => e.IsActive);
        }

        // Filter by status and created date (CreatedOn stored as UTC or local, so compare using UTC)
        query = query.Where(e => e.Status != null && EF.Functions.ILike(e.Status, normalizedStatus) && e.UpdatedOn.ToUniversalTime() <= upToUtc);

        return await query.OrderBy(e => e.EnrollmentId).ToListAsync(cancellationToken);
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
            .Include(e => e.Personal)
            .Include(e => e.Employment.Designation)
            .Include(e => e.Employment.Shift)
            .Include(e => e.Employment.EmployeeNature)
            .Include(e => e.Payroll).ThenInclude(p => p.SalaryAccount).ThenInclude(sa => sa.Banking)

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
            .Include(e => e.Employment.Unit)
            .Include(e => e.Employment.Department)
            .Include(e => e.Employment.Section)
            .Include(e => e.Employment.Cell)
            .Include(e => e.Employment.Grade)
            .Include(e => e.Employment.Designation)
            .Include(e => e.Employment.Shift)
            .Include(e => e.Payroll).ThenInclude(p => p.SalaryAccount)
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
                        || EF.Functions.ILike(e.EmployeeName, searchPattern)
                        || (e.EmployeeNameBangla != null) && EF.Functions.ILike(e.EmployeeNameBangla, searchPattern)
                        //|| (e.PersonalInfo != null && EF.Functions.Like(e.PersonalInfo.MobileNumber, searchPattern))
                        ))
            .OrderBy(e => e.EmployeeName)
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

    public async Task<string> GetNextEnrollmentIdAsync(DateTime todayUtc, CancellationToken cancellationToken = default)
    {
        var datePart = todayUtc.Date.ToString("ddMMyy");
        var key = $"lastEnrollmentId:{datePart}";

        // Use a transaction to read/update the lookup key/value row for today
        await using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable, cancellationToken);

        var kv = await _context.Set<LookKeyValue>()
            .FirstOrDefaultAsync(k => k.Key == key, cancellationToken);

        int nextSeq = 1;
        if (kv == null)
        {
            var value = $"{datePart}{nextSeq:D3}";
            kv = new LookKeyValue
            {
                Key = key,
                Value = value,
                IsActive = true,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

            _context.Set<LookKeyValue>().Add(kv);
        }
        else
        {
            // parse existing value's sequence suffix
            var existing = kv.Value ?? string.Empty;
            if (existing.Length >= datePart.Length + 1 && existing.StartsWith(datePart))
            {
                var seqPart = existing.Substring(datePart.Length);
                if (int.TryParse(seqPart, out var seq))
                {
                    nextSeq = seq + 1;
                }
                else
                {
                    nextSeq = 1;
                }
            }
            else
            {
                nextSeq = 1;
            }

            kv.Value = $"{datePart}{nextSeq:D3}";
            kv.UpdatedOn = DateTime.UtcNow;
            _context.Set<LookKeyValue>().Update(kv);
        }

        await _context.SaveChangesAsync(cancellationToken);

        var nextEnrollmentId = kv.Value!;

        await transaction.CommitAsync(cancellationToken);

        return nextEnrollmentId;
    }
}
