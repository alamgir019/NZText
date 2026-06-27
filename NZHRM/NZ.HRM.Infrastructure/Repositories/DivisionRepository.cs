using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class DivisionRepository : IDivisionRepository
{
    private readonly ApplicationDbContext _context;

    public DivisionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<LookDivision>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Divisions
            .Where(d => d.IsActive)
            .OrderBy(d => d.DivisionName)
            .ToListAsync(cancellationToken);
    }

    public async Task<LookDivision?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Divisions
            .FirstOrDefaultAsync(d => d.Id == id && d.IsActive, cancellationToken);
    }
}
