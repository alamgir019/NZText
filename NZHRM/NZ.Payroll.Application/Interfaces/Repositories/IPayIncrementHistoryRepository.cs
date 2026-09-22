using NZ.HRM.Domain.Entities;

namespace NZ.Payroll.Application.Interfaces.Repositories;

public interface IPayIncrementHistoryRepository
{
	Task<PayIncrementHistory> AddAsync(PayIncrementHistory entity, CancellationToken cancellationToken = default);
	Task<IEnumerable<PayIncrementHistory>> AddRangeAsync(IEnumerable<PayIncrementHistory> entities, CancellationToken cancellationToken = default);
	Task<IEnumerable<PayIncrementHistory>> AddHistoriesWithRequestsAsync(
		IEnumerable<PayIncrementHistory> histories,
		IEnumerable<PerIncrementRequest> requests,
		CancellationToken cancellationToken = default);
	Task<PayIncrementHistory?> GetByIdAsync(
		string id,
		CancellationToken cancellationToken = default);
	Task<IEnumerable<PayIncrementHistory>> GetByIdsAsync(
		IEnumerable<string> ids,
		CancellationToken cancellationToken = default);
	Task<PerIncrementRequest> UpdateHistoryWithRequestAsync(
		PayIncrementHistory history,
		PerIncrementRequest request,
		CancellationToken cancellationToken = default);
	Task<IEnumerable<PerIncrementRequest>> UpdateHistoriesWithRequestsAsync(
		IEnumerable<PayIncrementHistory> histories,
		IEnumerable<PerIncrementRequest> requests,
		CancellationToken cancellationToken = default);
	Task<IEnumerable<PerIncrementRequest>> UpdateHistoriesWithRequestsAndSalaryAsync(
		IEnumerable<PayIncrementHistory> histories,
		IEnumerable<PerIncrementRequest> requests,
		CancellationToken cancellationToken = default);
	Task<IEnumerable<PayIncrementHistory>> GetByStatusWithRequestsAsync(
		string status,
		CancellationToken cancellationToken = default);
	Task<IEnumerable<PayIncrementHistory>> GetPreviousHistoriesAsync(
		IEnumerable<string> employeeIds,
		IEnumerable<string> currentHistoryIds,
		CancellationToken cancellationToken = default);
	Task<bool> ExistsByEmployeeAndEffectiveDateAsync(string employeeId, DateOnly effectiveDate, CancellationToken cancellationToken = default);
}
