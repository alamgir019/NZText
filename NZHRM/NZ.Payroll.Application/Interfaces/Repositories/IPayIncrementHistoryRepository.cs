using NZ.HRM.Domain.Entities;

namespace NZ.Payroll.Application.Interfaces.Repositories;

public interface IPayIncrementHistoryRepository
{
	Task<PayIncrementHistory> AddAsync(PayIncrementHistory entity, CancellationToken cancellationToken = default);
	Task<bool> ExistsByEmployeeAndEffectiveDateAsync(string employeeId, DateOnly effectiveDate, CancellationToken cancellationToken = default);
}
