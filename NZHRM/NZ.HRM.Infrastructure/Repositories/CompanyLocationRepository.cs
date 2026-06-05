using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class CompanyLocationRepository : ICompanyLocationRepository
{
    private readonly ApplicationDbContext _context;

    public CompanyLocationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    //public async Task<List<CompanyLocation>> GetAllAsync(bool includeInactive = false, string? companyId = null, string? locationId = null, CancellationToken cancellationToken = default)
    //{
    //    var query = _context.CompanyLocations
    //        .Include(cl => cl.Company)
    //        .Include(cl => cl.Location)
    //        .AsQueryable();

    //    if (!includeInactive)
    //        query = query.Where(cl => cl.IsActive);

    //    if (!string.IsNullOrWhiteSpace(companyId))
    //        query = query.Where(cl => cl.CompanyId == companyId);

    //    if (!string.IsNullOrWhiteSpace(locationId))
    //        query = query.Where(cl => cl.LocationId == locationId);

    //    return await query
    //        .OrderBy(cl => cl.Company != null ? cl.Company.CompanyName : string.Empty)
    //        .ThenBy(cl => cl.Location != null ? cl.Location.LocationName : string.Empty)
    //        .ToListAsync(cancellationToken);
    //}

    //public async Task<CompanyLocation?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    //{
    //    return await _context.CompanyLocations
    //        .Include(cl => cl.Company)
    //        .Include(cl => cl.Location)
    //        .FirstOrDefaultAsync(cl => cl.Id == id && cl.IsActive, cancellationToken);
    //}

    //public async Task<string> AddAsync(CompanyLocation companyLocation, CancellationToken cancellationToken = default)
    //{
    //    _context.CompanyLocations.Add(companyLocation);
    //    await _context.SaveChangesAsync(cancellationToken);
    //    return companyLocation.Id;
    //}

    //public async Task UpdateAsync(CompanyLocation companyLocation, CancellationToken cancellationToken = default)
    //{
    //    _context.CompanyLocations.Update(companyLocation);
    //    await _context.SaveChangesAsync(cancellationToken);
    //}

    //public async Task DeleteAsync(CompanyLocation companyLocation, CancellationToken cancellationToken = default)
    //{
    //    _context.CompanyLocations.Remove(companyLocation);
    //    await _context.SaveChangesAsync(cancellationToken);
    //}

    //public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    //{
    //    return await _context.CompanyLocations.AnyAsync(cl => cl.Id == id, cancellationToken);
    //}
}
