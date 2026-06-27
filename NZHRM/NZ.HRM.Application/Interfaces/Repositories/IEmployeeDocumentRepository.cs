using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IEmployeeDocumentRepository
{
    Task<List<HrmEmployeeDocument>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<HrmEmployeeDocument>> GetByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken = default);
    Task<HrmEmployeeDocument?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> AddAsync(HrmEmployeeDocument employeeDocument, CancellationToken cancellationToken = default);
    Task<string> AddRangeAsync(List<HrmEmployeeDocument> employeeDocuments, CancellationToken cancellationToken = default);
    Task UpdateAsync(HrmEmployeeDocument employeeDocument, CancellationToken cancellationToken = default);
    Task DeleteAsync(HrmEmployeeDocument employeeDocument, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}