using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IEmployeeVerificationRepository
{
    Task<EmployeeVerification?> GetByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken = default);
    Task<string> AddAsync(EmployeeVerification employeeVerification, CancellationToken cancellationToken = default);
    Task UpdateAsync(EmployeeVerification employeeVerification, CancellationToken cancellationToken = default);
}
