using NZ.HRM.Domain.Entities;
using NZ.HRM.Domain.Constants;
using NZ.Payroll.Application.Interfaces.Repositories;
using NZ.Payroll.Application.PayIncrementHistories.Commands;
using NZ.Payroll.Application.PayIncrementHistories.DTOs;

namespace NZ.Payroll.Application.PayIncrementHistories.Handlers;

public class UpdatePayIncrementHistoryHandler
{
	private readonly IPayIncrementHistoryRepository _repository;

	public UpdatePayIncrementHistoryHandler(IPayIncrementHistoryRepository repository)
	{
		_repository = repository;
	}

	public async Task<List<UpdatePayIncrementHistoryResponseDto>> Handle(
		UpdateIncrementRequestsCommand command,
		string currentUser,
		CancellationToken cancellationToken = default)
	{
		if (string.IsNullOrWhiteSpace(currentUser))
			throw new UnauthorizedAccessException("Authenticated user was not found");

		if (command.Requests.Count == 0)
			throw new ArgumentException("At least one increment update request is required");

		var duplicateIds = command.Requests
			.GroupBy(request => request.PayIncrementHistoryId, StringComparer.OrdinalIgnoreCase)
			.Where(group => string.IsNullOrWhiteSpace(group.Key) || group.Count() > 1)
			.Select(group => group.Key)
			.ToList();

		if (duplicateIds.Count > 0)
			throw new InvalidOperationException("Duplicate or empty pay increment history IDs are not allowed");

		var historyIds = command.Requests
			.Select(request => request.PayIncrementHistoryId)
			.ToList();

		var histories = await _repository.GetByIdsAsync(historyIds, cancellationToken);
		var historyById = histories.ToDictionary(
			history => history.Id,
			StringComparer.OrdinalIgnoreCase);

		var missingIds = historyIds
			.Where(id => !historyById.ContainsKey(id))
			.ToList();

		if (missingIds.Count > 0)
			throw new KeyNotFoundException(
				$"Pay increment history records were not found: {string.Join(", ", missingIds)}");

		//var statuses = historyById.Values
		//	.Select(history => history.Status)
		//	.Distinct(StringComparer.OrdinalIgnoreCase)
		//	.ToList();

		//if (statuses.Count > 1)
		//	throw new InvalidOperationException("All pay increment histories in a batch must have the same status");


		var approvalDate = DateTime.UtcNow;
		var requests = new List<PerIncrementRequest>();

		foreach (var item in command.Requests)
		{
			var history = historyById[item.PayIncrementHistoryId];

			var currentStatus = history.Status;
			var nextStatuses = currentStatus switch
			{
				var status when string.Equals(status, PayIncrementStatuses.Pending, StringComparison.OrdinalIgnoreCase)
					=> new[] { PayIncrementStatuses.Approved, PayIncrementStatuses.Rejected },
				var status when string.Equals(status, PayIncrementStatuses.Approved, StringComparison.OrdinalIgnoreCase)
					=> new[] { PayIncrementStatuses.ForwardedToMovementSection },
				var status when string.Equals(status, PayIncrementStatuses.Rejected, StringComparison.OrdinalIgnoreCase)
					=> new[] { PayIncrementStatuses.ForwardedToMovementSection },
				var status when string.Equals(status, PayIncrementStatuses.ForwardedToMovementSection, StringComparison.OrdinalIgnoreCase)
					=> new[] { PayIncrementStatuses.ForwardedToHR },
				var status when string.Equals(status, PayIncrementStatuses.ForwardedToHR, StringComparison.OrdinalIgnoreCase)
					=> new[] { PayIncrementStatuses.ForwardedToCEO },
				_ => throw new InvalidOperationException($"Unsupported pay increment history status: '{currentStatus}'")
			};

			var requestedStatus = item.Status ?? string.Empty;
			var isValidNextStatus = nextStatuses.Any(status =>
				string.Equals(status, requestedStatus, StringComparison.OrdinalIgnoreCase));

			if (!isValidNextStatus)
				throw new ArgumentException($"Expected one of [{string.Join(", ", nextStatuses)}] but got '{item.Status}'");

			//if (string.Equals(nextStatus, PayIncrementStatuses.Approved, StringComparison.OrdinalIgnoreCase) &&
			//	(!item.NewGrossSalary.HasValue || item.NewGrossSalary.Value <= 2000m))
			//	throw new ArgumentException("New gross salary must be greater than 2000 when approving a pay increment");
			history.EffectiveDate = item.EffectiveDate ?? history.EffectiveDate;
            history.EffectiveDate = item.EffectiveDate?? history.EffectiveDate;
			history.OldGrossSalary = item.OldGrossSalary?? history.OldGrossSalary;
			history.NewGrossSalary = item.NewGrossSalary ?? history.NewGrossSalary;
			history.IncrementAmount = item.IncrementAmount ?? history.IncrementAmount;
			history.IncrementPercent = item.IncrementPercent ?? history.IncrementPercent;
			history.IncrementType = item.IncrementType ?? history.IncrementType;
			history.Status = item.Status ?? history.Status;
			history.UpdatedBy = currentUser;
			history.UpdatedOn = approvalDate;

			requests.Add(new PerIncrementRequest
			{
				PayIncHistId = history.Id,
				ApprovedBy = currentUser,
				ApprovalDate = approvalDate,
				FromStatus = currentStatus,
				ToStatus = item.Status ?? history.Status,
                CreatedBy = currentUser,
				UpdatedBy = currentUser,
				IsActive = true
			});
		}

		var savedRequests = await _repository.UpdateHistoriesWithRequestsAndSalaryAsync(
			histories,
			requests,
			cancellationToken);

		var requestByHistoryId = savedRequests.ToDictionary(
			request => request.PayIncHistId,
			StringComparer.OrdinalIgnoreCase);

		return command.Requests.Select(item =>
		{
			var history = historyById[item.PayIncrementHistoryId];
			var request = requestByHistoryId[history.Id];

			return new UpdatePayIncrementHistoryResponseDto
			{
				Id = history.Id,
				RequestId = request.Id,
				EmployeeId = history.EmployeeId,
				EffectiveDate = history.EffectiveDate,
				OldGrossSalary = history.OldGrossSalary,
				NewGrossSalary = history.NewGrossSalary,
				IncrementAmount = history.IncrementAmount,
				IncrementPercent = history.IncrementPercent,
				IncrementType = history.IncrementType,
				ApprovedBy = request.ApprovedBy ?? string.Empty,
				ApprovalDate = request.ApprovalDate,
				FromStatus = request.FromStatus,
				ToStatus = request.ToStatus
            };
		}).ToList();
	}
}
