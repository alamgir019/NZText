using NZ.HRM.Domain.Constants;
using NZ.Payroll.Application.Interfaces.Repositories;
using NZ.Payroll.Application.PromotionIncrementRequests.DTOs;
using NZ.Payroll.Application.PromotionIncrementRequests.Queries;

namespace NZ.Payroll.Application.PromotionIncrementRequests.Handlers;

public class GetPromotionIncrementRequestsByStatusHandler
{
	private readonly IPromotionIncrementRequestRepository _repository;

	public GetPromotionIncrementRequestsByStatusHandler(IPromotionIncrementRequestRepository repository)
	{
		_repository = repository;
	}

	public Task<List<PromotionIncrementRequestListItemDto>> Handle(
		GetPromotionIncrementRequestsByStatusQuery query,
		CancellationToken cancellationToken = default)
	{
		if (string.IsNullOrWhiteSpace(query.Status))
			throw new ArgumentException("Status is required");

		var status = query.Status.Trim().ToUpperInvariant();

		if (!PromotionIncrementStatuses.All.Contains(status))
			throw new ArgumentException(
				$"Unsupported status '{query.Status}'. Allowed values: {string.Join(", ", PromotionIncrementStatuses.All)}");

		return _repository.GetListByStatusAsync(status, cancellationToken);
	}
}
