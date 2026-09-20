using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Constants;
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

		var employeeById = employees.ToDictionary(
			employee => employee.Id,
			StringComparer.OrdinalIgnoreCase);

		return histories.Select(history =>
		{
			var employee = employeeById.GetValueOrDefault(history.EmployeeId);

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
			_ => throw new ArgumentException($"Unsupported pay increment history status: '{status}'", nameof(status))
		};
	}
}
