using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Designations.Queries.GetAllDesignations;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class DesignationRepository : IDesignationRepository
{
    private readonly ApplicationDbContext _context;

    public DesignationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MstDesignation>> GetAllAsync(GetAllDesignationsQuery query, CancellationToken cancellationToken = default)
    {
        var designationsQuery = _context.MstDesignations.AsQueryable();

        if (!query.IncludeInactive)
            designationsQuery = designationsQuery.Where(d => d.IsActive);

        if (!string.IsNullOrEmpty(query.GradeId))
            designationsQuery = designationsQuery.Where(d => d.GradeId == query.GradeId);

        return await designationsQuery.OrderBy(d => d.SortOrder).ThenBy(d => d.DesignationName).ToListAsync(cancellationToken);
    }

    public async Task<MstDesignation?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.MstDesignations.FirstOrDefaultAsync(d => d.Id == id && d.IsActive, cancellationToken);
    }

    public async Task<string> AddAsync(MstDesignation designation, CancellationToken cancellationToken = default)
    {
        _context.MstDesignations.Add(designation);
        await _context.SaveChangesAsync(cancellationToken);
        return designation.Id;
    }

    public async Task UpdateAsync(MstDesignation designation, CancellationToken cancellationToken = default)
    {
        _context.MstDesignations.Update(designation);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MstDesignation designation, CancellationToken cancellationToken = default)
    {
        _context.MstDesignations.Remove(designation);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.MstDesignations.AnyAsync(d => d.Id == id, cancellationToken);
    }
}
