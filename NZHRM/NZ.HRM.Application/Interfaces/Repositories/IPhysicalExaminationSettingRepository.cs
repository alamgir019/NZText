using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IPhysicalExaminationSettingRepository
{
    Task<List<HrmPhysicalExaminationSetting>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<HrmPhysicalExaminationSetting?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(HrmPhysicalExaminationSetting setting, CancellationToken cancellationToken = default);
    Task UpdateAsync(HrmPhysicalExaminationSetting setting, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
