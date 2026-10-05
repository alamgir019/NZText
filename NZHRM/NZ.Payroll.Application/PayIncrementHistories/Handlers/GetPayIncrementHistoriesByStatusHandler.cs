using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Constants;
using NZ.HRM.Domain.Entities;
using NZ.Payroll.Application.Interfaces.Repositories;
using NZ.Payroll.Application.PayIncrementHistories.DTOs;
using NZ.Payroll.Application.PayIncrementHistories.Queries;

namespace NZ.Payroll.Application.PayIncrementHistories.Handlers;

public class GetPayIncrementHistoriesByStatusHandler
{
	private readonly IPayIncrementHistoryRepository _repository;
	private readonly IEmployeeMasterRepository _employeeRepository;

	public GetPayIncrementHistoriesByStatusHandler(
		IPayIncrementHistoryRepository repository,
		IEmployeeMasterRepository employeeRepository)
	{
		_repository = repository;
		_employeeRepository = employeeRepository;
	}

	public async Task<List<PayIncrementHistoryWithRequestsDto>> Handle(
		GetPayIncrementHistoriesByStatusQuery query,
		CancellationToken cancellationToken = default)
	{
		if (string.IsNullOrWhiteSpace(query.Status))
			throw new ArgumentException("Status is required", nameof(query.Status));

		var status = GetCanonicalStatus(query.Status);

		var histories = (await _repository.GetByStatusWithRequestsAsync(
			status,
			cancellationToken)).ToList();

		if (histories.Count == 0)
			return new List<PayIncrementHistoryWithRequestsDto>();

		var employeeIds = histories
			.Select(history => history.EmployeeId)
			.Distinct(StringComparer.OrdinalIgnoreCase)
			.ToList();

		var employees = await _employeeRepository.GetByIdsAsync(
			employeeIds,
			cancellationToken);

		var previousHistories = (await _repository.GetPreviousHistoriesAsync(
			employeeIds,
			histories.Select(history => history.Id),
			cancellationToken)).ToList();

		var employeeById = employees.ToDictionary(
			employee => employee.Id,
			StringComparer.OrdinalIgnoreCase);

		return histories.Select(history =>
		{
			var employee = employeeById.GetValueOrDefault(history.EmployeeId);
			var previousEmployeeHistories = previousHistories
				.Where(previousHistory =>
					string.Equals(previousHistory.EmployeeId, history.EmployeeId, StringComparison.OrdinalIgnoreCase) &&
					previousHistory.EffectiveDate.HasValue &&
					history.EffectiveDate.HasValue &&
					previousHistory.EffectiveDate.Value < history.EffectiveDate.Value &&
					string.Equals(previousHistory.Status, PayIncrementStatuses.Approved, StringComparison.OrdinalIgnoreCase))
				.OrderByDescending(previousHistory => previousHistory.EffectiveDate)
				.ToList();
			var previousFivePercentIncrement = previousEmployeeHistories
				.FirstOrDefault(IsFivePercentIncrement);
			var lastPerformanceIncrement = previousEmployeeHistories
				.FirstOrDefault(IsPerformanceIncrement);
			var submittedOn = history.PerIncrementRequests
				.OrderByDescending(request => request.CreatedOn)
				.Select(request => (DateTime?)request.CreatedOn)
				.FirstOrDefault();

			return new PayIncrementHistoryWithRequestsDto
			{
				Id = history.Id,
				EmployeeId = history.EmployeeId,
				EmployeeCode = employee?.EmployeeCode ?? string.Empty,
				EmployeeName = employee?.EmployeeName ?? string.Empty,
				DepartmentName = employee?.Employment?.Department?.DepartmentName ?? string.Empty,
				SectionName = employee?.Employment?.Section?.SectionName ?? string.Empty,
				EffectiveDate = history.EffectiveDate,
				OldGrossSalary = history.OldGrossSalary,
				NewGrossSalary = history.NewGrossSalary,
				IncrementAmount = history.IncrementAmount,
				IncrementPercent = history.IncrementPercent,
				IncrementType = history.IncrementType,
				PreviousFivePercentIncrementDate = previousFivePercentIncrement?.EffectiveDate,
				PreviousFivePercentIncrementAmount = previousFivePercentIncrement?.IncrementAmount,
				LastPerformanceIncrementDate = lastPerformanceIncrement?.EffectiveDate,
				LastPerformanceIncrementPercent = lastPerformanceIncrement?.IncrementPercent,
				ProposedPerformanceIncrementPercent = IsPerformanceIncrement(history)
					? history.IncrementPercent
					: null,
				ProposedPerformanceIncrementAmount = IsPerformanceIncrement(history)
					? history.IncrementAmount
					: null,
				SubmittedOn = submittedOn,
				Status = history.Status,
				Requests = history.PerIncrementRequests
					.Select(request => new PerIncrementRequestDto
					{
						Id = request.Id,
						PayIncHistId = request.PayIncHistId,
						ApprovedBy = request.ApprovedBy,
						ApprovalDate = request.ApprovalDate,
						CreatedOn = request.CreatedOn,
						CreatedBy = request.CreatedBy,
						UpdatedOn = request.UpdatedOn,
						UpdatedBy = request.UpdatedBy,
						IsActive = request.IsActive
					})
					.ToList()
			};
		}).ToList();
	}

	private static bool IsFivePercentIncrement(PayIncrementHistory history)
	{
		return history.IncrementPercent == 5m && !IsPerformanceIncrement(history);
	}

	private static bool IsPerformanceIncrement(PayIncrementHistory history)
	{
		return history.IncrementType?.Contains("performance", StringComparison.OrdinalIgnoreCase) == true;
	}

	private static string GetCanonicalStatus(string status)
	{
		return status.Trim() switch
		{
			var value when string.Equals(value, PayIncrementStatuses.Pending, StringComparison.OrdinalIgnoreCase)
				=> PayIncrementStatuses.Pending,
			var value when string.Equals(value, PayIncrementStatuses.Forwarded, StringComparison.OrdinalIgnoreCase)
				=> PayIncrementStatuses.Forwarded,
            var value when string.Equals(value, PayIncrementStatuses.Approved, StringComparison.OrdinalIgnoreCase)
                => PayIncrementStatuses.Approved,
            var value when string.Equals(value, PayIncrementStatuses.ForwardedToMovementSection, StringComparison.OrdinalIgnoreCase)
                => PayIncrementStatuses.ForwardedToMovementSection,
            var value when string.Equals(value, PayIncrementStatuses.ForwardedToHR, StringComparison.OrdinalIgnoreCase)
                => PayIncrementStatuses.ForwardedToHR,
            var value when string.Equals(value, PayIncrementStatuses.ForwardedToCEO, StringComparison.OrdinalIgnoreCase)
                => PayIncrementStatuses.ForwardedToCEO,
            _ => throw new ArgumentException($"Unsupported pay increment history status: '{status}'", nameof(status))
		};
	}
}
