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

    public async Task<List<MedicalFitnessCheck>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.MedicalFitnessChecks.AsQueryable();

        if (!includeInactive)
            query = query.Where(x => x.IsActive);

        return await query
            .OrderByDescending(x => x.ExaminationDateTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<MedicalFitnessCheck>> GetByEmployeeIdAsync(string employeeId, bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.MedicalFitnessChecks
            .Where(x => x.EmployeeId == employeeId)
            .AsQueryable();

        if (!includeInactive)
            query = query.Where(x => x.IsActive);

        return await query
            .OrderByDescending(x => x.ExaminationDateTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<MedicalFitnessCheck?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.MedicalFitnessChecks
            .FirstOrDefaultAsync(x => x.Id == id && x.IsActive, cancellationToken);
    }

    public async Task<MedicalFitnessCheck?> GetLatestByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken = default)
    {
        return await _context.MedicalFitnessChecks
            .Where(x => x.EmployeeId == employeeId && x.IsActive)
            .OrderByDescending(x => x.ExaminationDateTime)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<string> AddAsync(MedicalFitnessCheck medicalFitnessCheck, CancellationToken cancellationToken = default)
    {
        _context.MedicalFitnessChecks.Add(medicalFitnessCheck);
        await _context.SaveChangesAsync(cancellationToken);
        return medicalFitnessCheck.Id;
    }

    public async Task UpdateAsync(MedicalFitnessCheck medicalFitnessCheck, CancellationToken cancellationToken = default)
    {
        _context.MedicalFitnessChecks.Update(medicalFitnessCheck);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.MedicalFitnessChecks.AnyAsync(x => x.Id == id, cancellationToken);
    }
}
