using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IGradeRepository
{
    Task<List<MstGrade>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default);
    Task<MstGrade?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(MstGrade grade, CancellationToken cancellationToken = default);
    Task UpdateAsync(MstGrade grade, CancellationToken cancellationToken = default);
    Task DeleteAsync(MstGrade grade, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
