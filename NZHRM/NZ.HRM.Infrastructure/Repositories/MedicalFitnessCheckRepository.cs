using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class MedicalFitnessCheckRepository : IMedicalFitnessCheckRepository
{
    private readonly ApplicationDbContext _context;

    public MedicalFitnessCheckRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<HrmMedicalFitnessCheck>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.HrmMedicalFitnessChecks.AsQueryable();

        if (!includeInactive)
            query = query.Where(x => x.IsActive);

        return await query
            .OrderByDescending(x => x.ExaminationDateTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<HrmMedicalFitnessCheck>> GetByEmployeeIdAsync(string employeeId, bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.HrmMedicalFitnessChecks
            .Where(x => x.EmployeeId == employeeId)
            .AsQueryable();

        if (!includeInactive)
            query = query.Where(x => x.IsActive);

        return await query
            .OrderByDescending(x => x.ExaminationDateTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<HrmMedicalFitnessCheck?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.HrmMedicalFitnessChecks
            .FirstOrDefaultAsync(x => x.Id == id && x.IsActive, cancellationToken);
    }

    public async Task<HrmMedicalFitnessCheck?> GetLatestByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken = default)
    {
        return await _context.HrmMedicalFitnessChecks
            .Where(x => x.EmployeeId == employeeId && x.IsActive)
            .OrderByDescending(x => x.ExaminationDateTime)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<string>> AddRangeAsync(List<HrmMedicalFitnessCheck> medicalFitnessChecks, CancellationToken cancellationToken = default)
    {
        await _context.HrmMedicalFitnessChecks.AddRangeAsync(medicalFitnessChecks, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return medicalFitnessChecks.Select(x => x.Id);
    }

    public async Task UpdateAsync(HrmMedicalFitnessCheck medicalFitnessCheck, CancellationToken cancellationToken = default)
    {
        _context.HrmMedicalFitnessChecks.Update(medicalFitnessCheck);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.HrmMedicalFitnessChecks.AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<string> AddAsync(HrmMedicalFitnessCheck newVersion, CancellationToken cancellationToken)
    {
        await _context.HrmMedicalFitnessChecks.AddAsync(newVersion, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return newVersion.Id;
    }
}
