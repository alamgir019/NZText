using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IEmployeeTransferRepository
{
	Task<List<HrmEmployeeTransfer>> GetAllAsync(
		bool includeInactive = false,
		string? employeeId = null,
		DateOnly? fromDate = null,
		DateOnly? toDate = null,
		CancellationToken cancellationToken = default);

	Task<HrmEmployeeTransfer?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

	Task<List<HrmEmployeeTransfer>> GetByEmployeeIdAsync(
		string employeeId,
		bool includeInactive = false,
		DateOnly? fromDate = null,
		DateOnly? toDate = null,
		CancellationToken cancellationToken = default);

	Task<HrmEmployeeTransfer?> GetLatestByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken = default);

	Task<string> AddWithEmploymentAsync(
		HrmEmployeeTransfer transfer,
		HrmEmployeeEmployment employment,
		CancellationToken cancellationToken = default);

	Task UpdateWithEmploymentAsync(
		HrmEmployeeTransfer transfer,
		HrmEmployeeEmployment employment,
		CancellationToken cancellationToken = default);

	Task DeleteWithEmploymentAsync(
		HrmEmployeeTransfer transfer,
		HrmEmployeeEmployment employment,
		CancellationToken cancellationToken = default);

	Task<bool> ExistsActiveForEmployeeAndEffectiveDateAsync(
		string employeeId,
		DateOnly effectiveFrom,
		string? excludeId = null,
		CancellationToken cancellationToken = default);
}
