using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class GradeRepository : IGradeRepository
{
    private readonly ApplicationDbContext _context;

    public GradeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<MstGrade>> GetAllAsync(bool includeInactive = false, string? employeeType = null, CancellationToken cancellationToken = default)
    {
        var query = _context.MstGrades.AsQueryable();

        if (!includeInactive)
        {
            query = query.Where(g => g.IsActive);
        }

        if (!string.IsNullOrWhiteSpace(employeeType))
        {
            query = query.Where(g => g.EmployeeType == employeeType);
        }

        return await query
            .OrderBy(g => g.GradeName)
            .ToListAsync(cancellationToken);
    }

    public async Task<MstGrade?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.MstGrades
            .FirstOrDefaultAsync(g => g.Id == id && g.IsActive, cancellationToken);
    }

    public async Task<string> AddAsync(MstGrade grade, CancellationToken cancellationToken = default)
    {
        _context.MstGrades.Add(grade);
        await _context.SaveChangesAsync(cancellationToken);
        return grade.Id;
    }

    public async Task UpdateAsync(MstGrade grade, CancellationToken cancellationToken = default)
    {
        _context.MstGrades.Update(grade);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MstGrade grade, CancellationToken cancellationToken = default)
    {
        _context.MstGrades.Remove(grade);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.MstGrades
            .AnyAsync(g => g.Id == id, cancellationToken);
    }
}
