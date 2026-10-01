using NZ.HRM.Domain.Entities;
using NZ.Payroll.Application.PromotionIncrementRequests.DTOs;

namespace NZ.Payroll.Application.Interfaces.Repositories;

public interface IPromotionIncrementRequestRepository
{
	Task<List<PromotionIncrementEmployeeSnapshot>> GetEmployeeSnapshotsAsync(
		IReadOnlyCollection<string> employeeIds,
		CancellationToken cancellationToken = default);
	Task<HashSet<string>> GetExistingDesignationIdsAsync(
		IReadOnlyCollection<string> designationIds,
		CancellationToken cancellationToken = default);
	Task<HashSet<string>> GetExistingGradeIdsAsync(
		IReadOnlyCollection<string> gradeIds,
		CancellationToken cancellationToken = default);
	Task<List<string>> GetEmployeeIdsWithOpenRequestsAsync(
		IReadOnlyCollection<string> employeeIds,
		CancellationToken cancellationToken = default);
	Task AddRangeAsync(
		IReadOnlyCollection<PayPromotionIncrementRequest> requests,
		CancellationToken cancellationToken = default);
	Task<List<PromotionIncrementRequestListItemDto>> GetListByStatusAsync(
		string status,
		CancellationToken cancellationToken = default);
	Task<List<PayPromotionIncrementRequest>> GetByIdsAsync(
		IReadOnlyCollection<string> requestIds,
		CancellationToken cancellationToken = default);
	Task SaveTransitionsAsync(
		IReadOnlyCollection<PayPromotionIncrementRequest> requests,
		IReadOnlyCollection<PayPromotionIncrementApprovalHistory> histories,
		CancellationToken cancellationToken = default);
	Task<bool> ExistsAsync(
		string requestId,
		CancellationToken cancellationToken = default);
	Task<List<PayPromotionIncrementApprovalHistory>> GetApprovalHistoryAsync(
		string requestId,
		CancellationToken cancellationToken = default);
}
