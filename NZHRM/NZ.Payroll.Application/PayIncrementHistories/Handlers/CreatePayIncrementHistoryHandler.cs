using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Domain.Constants;
using NZ.Payroll.Application.Interfaces.Repositories;
using NZ.Payroll.Application.PayIncrementHistories.Commands;

namespace NZ.Payroll.Application.PayIncrementHistories.Handlers;

public class CreatePayIncrementHistoryHandler
{
	private readonly IPayIncrementHistoryRepository _repository;
	private readonly IEmployeeMasterRepository _employeeQuery;

	public CreatePayIncrementHistoryHandler(IPayIncrementHistoryRepository repository, IEmployeeMasterRepository employeeQuery)
	{
		_repository = repository;
		_employeeQuery = employeeQuery;
	}

	public async Task<List<string>> Handle(CreateIncrementRequestsCommand command, CancellationToken cancellationToken = default)
	{
		if (string.IsNullOrWhiteSpace(command.CreatedBy))
			throw new UnauthorizedAccessException("Authenticated user was not found");

		if (command.Requests.Count == 0)
			throw new ArgumentException("At least one increment request is required");

		var histories = new List<PayIncrementHistory>();
		var perIncrementRequests = new List<PerIncrementRequest>();
		var approvalDate = DateTime.UtcNow;

		foreach (var request in command.Requests)
		{
			if (string.IsNullOrWhiteSpace(request.EmployeeId))
				throw new ArgumentException("Employee ID is required", nameof(request.EmployeeId));

			var employeeExists = await _employeeQuery.ExistsAsync(request.EmployeeId, cancellationToken);
			if (!employeeExists)
				throw new KeyNotFoundException($"Employee with ID '{request.EmployeeId}' not found");

			var duplicateExists = await _repository.ExistsByEmployeeAndEffectiveDateAsync(request.EmployeeId, request.EffectiveDate, cancellationToken);
			if (duplicateExists)
				throw new InvalidOperationException($"An increment history record already exists for employee '{request.EmployeeId}' with effective date '{request.EffectiveDate}'");



			var history = new PayIncrementHistory
			{
				EmployeeId = request.EmployeeId,
				EffectiveDate = request.EffectiveDate,
				OldGrossSalary = request.OldGrossSalary,
				NewGrossSalary = request.NewGrossSalary,
				IncrementAmount = request.IncrementAmount,
				IncrementPercent = request.IncrementPercent,
				IncrementType = request.IncrementType,
				Status = PayIncrementStatuses.Pending,
				IsActive = true
			};
			histories.Add(history);

			var perRequest = new PerIncrementRequest
			{
				PayIncHistId = history.Id,
				ApprovedBy = command.CreatedBy,
				ApprovalDate = approvalDate,
				IsActive = true
			};

			perIncrementRequests.Add(perRequest);
		}

		var savedHistories = await _repository.AddHistoriesWithRequestsAsync(
			histories,
			perIncrementRequests,
			cancellationToken);

		return savedHistories.Select(e => e.Id).ToList();
	}
}
