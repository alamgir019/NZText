using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IMedicalFitnessCheckRepository
{
    Task<List<MedicalFitnessCheck>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<List<MedicalFitnessCheck>> GetByEmployeeIdAsync(string employeeId, bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<MedicalFitnessCheck?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<MedicalFitnessCheck?> GetLatestByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken = default);
    Task<string> AddAsync(MedicalFitnessCheck medicalFitnessCheck, CancellationToken cancellationToken = default);
    Task UpdateAsync(MedicalFitnessCheck medicalFitnessCheck, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
