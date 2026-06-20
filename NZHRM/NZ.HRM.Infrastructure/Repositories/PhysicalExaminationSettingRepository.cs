using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class PhysicalExaminationSettingRepository : IPhysicalExaminationSettingRepository
{
    private readonly ApplicationDbContext _context;

    public PhysicalExaminationSettingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<HrmPhysicalExaminationSetting>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.HrmPhysicalExaminationSettings.AsQueryable();

        if (!includeInactive)
            query = query.Where(x => x.IsActive);

        return await query
            .OrderBy(x => x.DisplayOrder)
            .ThenBy(x => x.FieldName)
            .ToListAsync(cancellationToken);
    }

    public async Task<HrmPhysicalExaminationSetting?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.HrmPhysicalExaminationSettings
            .FirstOrDefaultAsync(x => x.Id == id && x.IsActive, cancellationToken);
    }

    public async Task<string> AddAsync(HrmPhysicalExaminationSetting setting, CancellationToken cancellationToken = default)
    {
        _context.HrmPhysicalExaminationSettings.Add(setting);
        await _context.SaveChangesAsync(cancellationToken);
        return setting.Id;
    }

    public async Task UpdateAsync(HrmPhysicalExaminationSetting setting, CancellationToken cancellationToken = default)
    {
        _context.HrmPhysicalExaminationSettings.Update(setting);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.HrmPhysicalExaminationSettings.AnyAsync(x => x.Id == id, cancellationToken);
    }
}
