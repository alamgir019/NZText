using NZ.HRM.Domain.Entities;

namespace NZ.Payroll.Application.Interfaces.Repositories;

public interface IPayrollAdjustmentHistoryRepository
{
    Task<PayPayrollAdjustmentHistory> AddAsync(PayPayrollAdjustmentHistory entity, CancellationToken cancellationToken = default);
}
