using NZ.HRM.Domain.Entities;
using NZ.Payroll.Application.Interfaces.Repositories;
using NZ.Payroll.Application.PromotionIncrementRequests.Commands;
using NZ.Payroll.Application.PromotionIncrementRequests.DTOs;

namespace NZ.Payroll.Application.PromotionIncrementRequests.Handlers;

public class ForwardPromotionIncrementRequestsToHrBranchManagerHandler
{
	private readonly IPromotionIncrementRequestRepository _repository;

	public ForwardPromotionIncrementRequestsToHrBranchManagerHandler(IPromotionIncrementRequestRepository repository)
	{
		_repository = repository;
	}

	public async Task<ForwardPromotionIncrementRequestsResultDto> Handle(
		ForwardPromotionIncrementRequestsToHrBranchManagerCommand command,
		string currentUser,
		CancellationToken cancellationToken = default)
	{
		if (string.IsNullOrWhiteSpace(currentUser))
			throw new UnauthorizedAccessException("Authenticated user was not found");

		var requestIds = (command.RequestIds ?? new List<string>())
			.Select(id => id?.Trim() ?? string.Empty)
			.ToList();

		if (requestIds.Count == 0)
			throw new ArgumentException("At least one promotion increment request must be selected");

		if (requestIds.Any(string.IsNullOrWhiteSpace) ||
			requestIds.Count != requestIds.Distinct(StringComparer.OrdinalIgnoreCase).Count())
			throw new ArgumentException("Duplicate or empty request IDs are not allowed");

		var requests = await _repository.GetByIdsAsync(requestIds, cancellationToken);
		var foundIds = requests.Select(request => request.Id).ToHashSet(StringComparer.OrdinalIgnoreCase);
		var missingIds = requestIds.Where(id => !foundIds.Contains(id)).ToList();

		if (missingIds.Count > 0)
			throw new KeyNotFoundException(
				$"Promotion increment requests were not found: {string.Join(", ", missingIds)}");

		var histories = new List<PayPromotionIncrementApprovalHistory>();
		foreach (var request in requests)
		{
			histories.Add(request.ForwardToHrBranchManager(currentUser, command.Remarks));
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
}
