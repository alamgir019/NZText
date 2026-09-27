using NZ.HRM.Domain.Entities;
using NZ.Payroll.Application.PayrollExceptions.DTOs;
using NZ.Payroll.Application.PayrollExceptions.Queries;

namespace NZ.Payroll.Application.Interfaces.Repositories;

public interface IPayrollExceptionRepository
{
    Task<PayrollExceptionRequestsResponseDto> GetPayrollExceptionRequestsAsync(GetPayrollExceptionRequestsQuery query, CancellationToken cancellationToken = default);
    Task<PayrollExceptionRequestDetailDto?> GetDetailByIdAsync(string requestId, CancellationToken cancellationToken = default);
    Task<List<PayPayrollException>> GetByIdsAsync(IReadOnlyCollection<string> ids, CancellationToken cancellationToken = default);
    Task SaveForwardingAsync(IReadOnlyCollection<PayPayrollException> requests, string processedBy, string? remarks, CancellationToken cancellationToken = default);
}
