using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Employees.Queries.GetEmployeeMasterList;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;
using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Infrastructure.Repositories;

public class EmployeeMasterRepository : IEmployeeMasterRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeMasterRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<HrmEmployeeMaster>> GetByStatusUpToDateAsync(Application.Employees.Queries.GetItActivationSummary.GetItActivationSummaryQuery query, CancellationToken cancellationToken = default)
    {
        var normalizedStatus = query.status?.Trim();

        if (string.IsNullOrWhiteSpace(normalizedStatus))
            return new List<HrmEmployeeMaster>();

        var result = _context.HrmEmployeeMasters
            .Include(e => e.Employment.Unit)
            .AsQueryable();

        if (!query.includeInactive)
        {
            result = result.Where(e => e.IsActive);
        }
        if (!string.IsNullOrWhiteSpace(query.UnitId))
        {
            result = result.Where(e => e.Employment.UnitId == query.UnitId);
        }
        if (!string.IsNullOrWhiteSpace(query.status))
        {
            result = result.Where(e => EF.Functions.ILike(e.Status, normalizedStatus));
        }
        if (query.date != default)
        {
            result = result.Where(e => e.UpdatedOn.ToUniversalTime() <= query.date.ToUniversalTime());
        }

        return await result.OrderBy(e => e.EnrollmentId).ToListAsync(cancellationToken);
    }

    public async Task<List<HrmEmployeeMaster>> GetAllAsync(DateTime? onDate = null, bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.HrmEmployeeMasters
            .Include(e => e.Employment.Unit)
            .Include(e => e.Employment.Department)
            .Include(e => e.Employment.Section)
            .Include(e => e.Employment.Grade)
            .Include(e => e.Employment.Shift)
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
            .AsQueryable();
        if (!includeInactive)
        {
            query = query.Where(e => e.IsActive);
        }

        return await query
            .OrderBy(e => e.EmployeeCode)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<HrmEmployeeMaster>> GetAllAsync(
        bool includeInactive = false,
        bool includeEmployment = false,
        bool includePersonal = false,
        bool includePayroll = false,
        bool includeNominees = false,
        bool includeVerification = false,
        bool includeMedical = false,
        bool includeDocuments = false,
        CancellationToken cancellationToken = default)
    {
        var query = _context.HrmEmployeeMasters.AsQueryable();

        // Dynamically include child entities based on parameters
        if (includeEmployment)
        {
            query = query.Include(e => e.Employment)
                .ThenInclude(emp => emp.Unit)
                .Include(e => e.Employment)
                .ThenInclude(emp => emp.Department)
                .Include(e => e.Employment)
                .ThenInclude(emp => emp.Section)
                .Include(e => e.Employment)
                .ThenInclude(emp => emp.Grade)
                .Include(e => e.Employment)
                .ThenInclude(emp => emp.Shift)
                .Include(e => e.Employment)
                .ThenInclude(emp => emp.Designation);
        }

        if (includePersonal)
        {
            query = query.Include(e => e.Personal)
                .ThenInclude(p => p.PermanentThana)
                .Include(e => e.Personal)
                .ThenInclude(p => p.PermanentDistrict)
                .Include(e => e.Personal)
                .ThenInclude(p => p.PermanentDivision)
                .Include(e => e.Personal)
                .ThenInclude(p => p.PresentThana)
                .Include(e => e.Personal)
                .ThenInclude(p => p.PresentDistrict)
                .Include(e => e.Personal)
                .ThenInclude(p => p.PresentDivision);
        }

        if (includePayroll)
        {
            query = query.Include(e => e.Payroll);
        }

        if (includeNominees)
        {
            query = query.Include(e => e.Nominees);
        }

        if (includeVerification)
        {
            query = query.Include(e => e.Verification);
        }

        if (includeMedical)
        {
            query = query.Include(e => e.MedicalFitnessCheck);
        }

        if (includeDocuments)
        {
            query = query.Include(e => e.Documents);
        }

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
            .Include(e => e.MedicalFitnessCheck) // req for medical report generation
            .Include(e => e.Documents) // req for medical report generation
            .Include(e => e.Employment.Unit)
            .Include(e => e.Employment.Subunit)
            .Include(e => e.Employment.Department)
            .Include(e => e.Employment.Section)
            .Include(e => e.Employment.Grade)
            .Include(e => e.Employment.Cell)
            .Include(e => e.Personal).ThenInclude(p => p.PermanentDivision)
            .Include(e => e.Personal).ThenInclude(p => p.PermanentDistrict)
            .Include(e => e.Personal).ThenInclude(p => p.PermanentThana)
            .Include(e => e.Personal).ThenInclude(p => p.PresentDivision)
            .Include(e => e.Personal).ThenInclude(p => p.PresentDistrict)
            .Include(e => e.Personal).ThenInclude(p => p.PresentThana)
            .Include(e => e.Employment.Designation)
            .Include(e => e.Employment.Shift)
            .Include(e => e.Nominees)
            .Include(e => e.Payroll).ThenInclude(p => p.SalaryAccount).ThenInclude(sa => sa.Banking)
            .FirstOrDefaultAsync(e => e.Id == id && e.IsActive, cancellationToken);
    }

    public async Task<HrmEmployeeMaster?> GetByEmployeeCodeAsync(string employeeCode, CancellationToken cancellationToken = default)
    {
        return await _context.HrmEmployeeMasters
            .Include(e => e.MedicalFitnessCheck) // req for medical report generation
            .Include(e => e.Documents) // req for medical report generation
            .Include(e => e.Employment.Unit)
            .Include(e => e.Employment.Subunit)
            .Include(e => e.Employment.Department)
            .Include(e => e.Employment.Section)
            .Include(e => e.Employment.Grade)
            .Include(e => e.Employment.Cell)
            .Include(e => e.Personal).ThenInclude(p => p.PermanentDivision)
            .Include(e => e.Personal).ThenInclude(p => p.PermanentDistrict)
            .Include(e => e.Personal).ThenInclude(p => p.PermanentThana)
            .Include(e => e.Personal).ThenInclude(p => p.PresentDivision)
            .Include(e => e.Personal).ThenInclude(p => p.PresentDistrict)
            .Include(e => e.Personal).ThenInclude(p => p.PresentThana)
            .Include(e => e.Employment.Designation)
            .Include(e => e.Employment.Shift)
            .Include(e => e.Nominees)
            .Include(e => e.Payroll)
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

    public async Task<(List<HrmEmployeeMaster> employees, int totalCount)> GetEmployeeMasterListAsync(
        GetEmployeeMasterListQuery filterRequest,
        CancellationToken cancellationToken = default)
    {
        if (filterRequest == null)
        {
            throw new ArgumentNullException(nameof(filterRequest));
        }

        var query = _context.HrmEmployeeMasters
            .Include(e => e.Employment.Unit)
            .Include(e => e.Employment.Subunit)
            .Include(e => e.Employment.Department)
            .Include(e => e.Employment.Section)
            .Include(e => e.Employment.Cell)
            .Include(e => e.Employment.Designation)
            .AsQueryable();

        // Apply active/inactive filter
        if (!filterRequest.IncludeInactive)
        {
            query = query.Where(e => e.IsActive);
        }

        if (!string.IsNullOrEmpty(filterRequest.EmployeeCode))
        {
            query = query.Where(e => e.EmployeeCode != null && EF.Functions.ILike(e.EmployeeCode, filterRequest.EmployeeCode));
        }

        if (!string.IsNullOrEmpty(filterRequest.EmployeeMobile))
        {
            query = query.Where(e => e.Personal != null && e.Personal.MobileNumber != null && EF.Functions.ILike(e.Personal.MobileNumber, filterRequest.EmployeeMobile));
        }

        if (!string.IsNullOrEmpty(filterRequest.IdNumber))
        {
            query = query.Where(e => e.Personal != null && e.Personal.IdNumber != null && EF.Functions.ILike(e.Personal.IdNumber, filterRequest.IdNumber));
        }

        if (filterRequest.Religion.HasValue)
        {
            query = query.Where(e => e.Personal != null && e.Personal.Religion == filterRequest.Religion.ToString());
        }

        if (filterRequest.Gender.HasValue)
        {
            query = query.Where(e => e.Personal != null && e.Personal.Gender == filterRequest.Gender.ToString());
        }

        // Apply Unit filter
        if (!string.IsNullOrEmpty(filterRequest.UnitId))
        {
            query = query.Where(e => e.Employment != null && e.Employment.UnitId == filterRequest.UnitId);
        }

        // Apply SubUnit filter
        if (!string.IsNullOrEmpty(filterRequest.SubUnitId))
        {
            query = query.Where(e => e.Employment != null && e.Employment.SubunitId == filterRequest.SubUnitId);
        }

        // Apply Department filter
        if (!string.IsNullOrEmpty(filterRequest.DepartmentId))
        {
            query = query.Where(e => e.Employment != null && e.Employment.DepartmentId == filterRequest.DepartmentId);
        }

        // Apply Section filter
        if (!string.IsNullOrEmpty(filterRequest.SectionId))
        {
            query = query.Where(e => e.Employment != null && e.Employment.SectionId == filterRequest.SectionId);
        }

        // Apply Cell filter
        if (!string.IsNullOrEmpty(filterRequest.CellId))
        {
            query = query.Where(e => e.Employment != null && e.Employment.CellId == filterRequest.CellId);
        }

        // Apply Employee Nature filter
        if (!string.IsNullOrEmpty(filterRequest.EmployeeNature))
        {
            query = query.Where(e => e.EmployeeNature != null && EF.Functions.ILike(e.EmployeeNature, filterRequest.EmployeeNature));
        }

        // Apply Joining Date From filter
        if (filterRequest.JoiningFromDate.HasValue)
        {
            query = query.Where(e => e.Employment != null && e.Employment.JoiningDate >= filterRequest.JoiningFromDate);
        }

        // Apply Joining Date To filter
        if (filterRequest.JoiningToDate.HasValue)
        {
            query = query.Where(e => e.Employment != null && e.Employment.JoiningDate <= filterRequest.JoiningToDate);
        }

        if(!string.IsNullOrEmpty(filterRequest.GradeId))
        {
            query = query.Where(e => e.Employment != null && e.Employment.GradeId == filterRequest.GradeId);
        }

        if(!string.IsNullOrEmpty(filterRequest.ShiftId))
        {
            query = query.Where(e => e.Employment != null && e.Employment.ShiftId == filterRequest.ShiftId);
        }

        if (!string.IsNullOrEmpty(filterRequest.DivisionId))
        {
            query = query.Where(e => e.Personal != null && e.Personal.PermanentDivisionId == filterRequest.DivisionId);
        }

        query = query.Where(e => e.Status == EmployeeStatus.ITActivation.ToString());

        // Get total count before pagination
        var totalCount = await query.CountAsync(cancellationToken);

        // Apply pagination
        var validPageNumber = Math.Max(1, filterRequest.PageNumber);
        var validPageSize = Math.Max(1, Math.Min(filterRequest.PageSize, 1000)); // Cap at 1000
        var skip = (validPageNumber - 1) * validPageSize;

        var employees = await query
            .OrderBy(e => e.EmployeeCode)
            .Skip(skip)
            .Take(validPageSize)
            .ToListAsync(cancellationToken);

        return (employees, totalCount);
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

    public async Task<bool> IsEmployeeCodeUniqueAsync(string employeeCode, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(employeeCode))
            return false;

        var query = _context.HrmEmployeeMasters
            .Where(e => e.EmployeeCode == employeeCode);

        // Return true if NO matching code exists (code is unique)
        return !await query.AnyAsync(cancellationToken);
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
