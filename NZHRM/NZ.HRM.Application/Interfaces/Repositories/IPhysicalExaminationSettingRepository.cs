using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IPhysicalExaminationSettingRepository
{
    Task<List<PhysicalExaminationSetting>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<PhysicalExaminationSetting?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(PhysicalExaminationSetting setting, CancellationToken cancellationToken = default);
    Task UpdateAsync(PhysicalExaminationSetting setting, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
