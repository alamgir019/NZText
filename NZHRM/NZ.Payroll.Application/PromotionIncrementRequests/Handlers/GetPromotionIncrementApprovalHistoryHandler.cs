using NZ.Payroll.Application.Interfaces.Repositories;
using NZ.Payroll.Application.PromotionIncrementRequests.DTOs;
using NZ.Payroll.Application.PromotionIncrementRequests.Queries;

namespace NZ.Payroll.Application.PromotionIncrementRequests.Handlers;

public class GetPromotionIncrementApprovalHistoryHandler
{
	private readonly IPromotionIncrementRequestRepository _repository;

	public GetPromotionIncrementApprovalHistoryHandler(IPromotionIncrementRequestRepository repository)
	{
		_repository = repository;
	}

	public async Task<List<PromotionIncrementApprovalHistoryDto>> Handle(
		GetPromotionIncrementApprovalHistoryQuery query,
		CancellationToken cancellationToken = default)
	{
		if (string.IsNullOrWhiteSpace(query.RequestId))
			throw new ArgumentException("Request ID is required");

		var requestId = query.RequestId.Trim();

		if (!await _repository.ExistsAsync(requestId, cancellationToken))
			throw new KeyNotFoundException($"Promotion increment request '{requestId}' was not found");

		var histories = await _repository.GetApprovalHistoryAsync(requestId, cancellationToken);

		return histories.Select(history => new PromotionIncrementApprovalHistoryDto
		{
			Id = history.Id,
			StepNo = history.StepNo,
			StepName = history.StepName,
			Action = history.Action,
			FromStatus = history.FromStatus,
			ToStatus = history.ToStatus,
			ActionBy = history.ActionBy,
			ActionOn = history.ActionOn,
			Remarks = history.Remarks
		}).ToList();
	}
}
