using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IEmployeeVerificationRepository
{
    Task<HrmEmployeeVerification?> GetByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken = default);
    Task<string> AddAsync(HrmEmployeeVerification employeeVerification, CancellationToken cancellationToken = default);
    Task UpdateAsync(HrmEmployeeVerification employeeVerification, CancellationToken cancellationToken = default);
}
