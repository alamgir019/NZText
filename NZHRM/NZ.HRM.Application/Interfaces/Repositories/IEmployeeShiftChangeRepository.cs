using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IEmployeeShiftChangeRepository
{
	Task<List<HrmEmployeeShiftChange>> GetAllAsync(
		bool includeInactive = false,
		string? employeeId = null,
		DateOnly? fromDate = null,
		DateOnly? toDate = null,
		CancellationToken cancellationToken = default);

	Task<HrmEmployeeShiftChange?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

	Task<List<HrmEmployeeShiftChange>> GetByEmployeeIdAsync(
		string employeeId,
		bool includeInactive = false,
		DateOnly? fromDate = null,
		DateOnly? toDate = null,
		CancellationToken cancellationToken = default);

	Task<HrmEmployeeShiftChange?> GetLatestByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken = default);

	Task<string> AddAsync(HrmEmployeeShiftChange shiftChange, CancellationToken cancellationToken = default);

	Task<string> AddWithEmploymentAsync(
		HrmEmployeeShiftChange shiftChange,
		HrmEmployeeEmployment employment,
		CancellationToken cancellationToken = default);

	Task UpdateAsync(HrmEmployeeShiftChange shiftChange, CancellationToken cancellationToken = default);

	Task UpdateWithEmploymentAsync(
		HrmEmployeeShiftChange shiftChange,
		HrmEmployeeEmployment employment,
		CancellationToken cancellationToken = default);

	Task DeleteAsync(HrmEmployeeShiftChange shiftChange, CancellationToken cancellationToken = default);

	Task DeleteWithEmploymentAsync(
		HrmEmployeeShiftChange shiftChange,
		HrmEmployeeEmployment employment,
		CancellationToken cancellationToken = default);

	Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);

	Task<bool> ExistsActiveForEmployeeAndEffectiveDateAsync(
		string employeeId,
		DateOnly effectiveFrom,
		string? excludeId = null,
		CancellationToken cancellationToken = default);
}
