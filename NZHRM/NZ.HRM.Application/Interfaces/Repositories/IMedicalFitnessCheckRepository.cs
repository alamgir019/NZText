using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IMedicalFitnessCheckRepository
{
    Task<List<HrmMedicalFitnessCheck>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<List<HrmMedicalFitnessCheck>> GetByEmployeeIdAsync(string employeeId, bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<HrmMedicalFitnessCheck?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<HrmMedicalFitnessCheck?> GetLatestByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken = default);
    Task<IEnumerable<string>> AddRangeAsync(List<HrmMedicalFitnessCheck> medicalFitnessChecks, CancellationToken cancellationToken = default);
    Task UpdateAsync(HrmMedicalFitnessCheck medicalFitnessCheck, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(HrmMedicalFitnessCheck newVersion, CancellationToken cancellationToken);
}
