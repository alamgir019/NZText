using NZ.HRM.Domain.Entities;
using NZ.Payroll.Application.Interfaces.Repositories;
using NZ.Payroll.Application.PromotionIncrementRequests.Commands;
using NZ.Payroll.Application.PromotionIncrementRequests.DTOs;

namespace NZ.Payroll.Application.PromotionIncrementRequests.Handlers;

public class ForwardPromotionIncrementRequestsToMovementCellHandler
{
	private readonly IPromotionIncrementRequestRepository _repository;

	public ForwardPromotionIncrementRequestsToMovementCellHandler(IPromotionIncrementRequestRepository repository)
	{
		_repository = repository;
	}

	public async Task<ForwardPromotionIncrementRequestsResultDto> Handle(
		ForwardPromotionIncrementRequestsToMovementCellCommand command,
		string currentUser, bool isCell = true,
		CancellationToken cancellationToken = default)
	{
		if (string.IsNullOrWhiteSpace(currentUser))
			throw new UnauthorizedAccessException("Authenticated user was not found");

		var requestsToProcess = Normalize(command.Requests);

		if (requestsToProcess.Count == 0)
			throw new ArgumentException("At least one promotion increment request must be selected");

		if (requestsToProcess.Count != requestsToProcess.Select(request => request.RequestId).Distinct(StringComparer.OrdinalIgnoreCase).Count())
			throw new ArgumentException("Duplicate or empty request IDs are not allowed");

		var requestIds = requestsToProcess.Select(request => request.RequestId).ToList();
		var requests = await _repository.GetByIdsAsync(requestIds, cancellationToken);
		var requestById = requests.ToDictionary(request => request.Id, StringComparer.OrdinalIgnoreCase);
		var missingIds = requestIds.Where(id => !requestById.ContainsKey(id)).ToList();

		if (missingIds.Count > 0)
			throw new KeyNotFoundException(
				$"Promotion increment requests were not found: {string.Join(", ", missingIds)}");

		var histories = new List<PayPromotionIncrementApprovalHistory>();
		foreach (var action in requestsToProcess)
		{
			var request = requestById[action.RequestId];

			histories.Add(action.Approved
				? isCell ? request.ForwardToEmployeeMovementCell(currentUser, action.Remarks) : request.ForwardToEmployeeMovementSection(currentUser, action.Remarks)
                : request.Reject(currentUser, action.Remarks));
		}

		await _repository.SaveTransitionsAsync(requests, histories, cancellationToken);

		return new ForwardPromotionIncrementRequestsResultDto
		{
			ForwardedCount = requests.Count,
			ForwardedOn = histories[0].ActionOn,
			Items = requests.Select(request => new ForwardedPromotionIncrementRequestDto
			{
				RequestId = request.Id,
				EmployeeId = request.EmployeeId,
				CurrentGrossSalary = request.CurrentGrossSalary,
				NewGrossSalary = request.NewGrossSalary,
				Status = request.Status
			}).ToList()
		};
	}

	private static List<PromotionIncrementRequestActionDto> Normalize(List<PromotionIncrementRequestActionDto>? requests)
		=> (requests ?? new List<PromotionIncrementRequestActionDto>())
			.Where(request => !string.IsNullOrWhiteSpace(request.RequestId))
			.Select(request =>
			{
				request.RequestId = request.RequestId.Trim();
				request.Remarks = string.IsNullOrWhiteSpace(request.Remarks) ? null : request.Remarks.Trim();
				return request;
			})
			.ToList();
}
