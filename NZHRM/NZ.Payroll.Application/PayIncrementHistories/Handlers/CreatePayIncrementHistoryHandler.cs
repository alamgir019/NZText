using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
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
		var histories = new List<PayIncrementHistory>();
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

			var entity = new PayIncrementHistory
			{
				EmployeeId = request.EmployeeId,
				EffectiveDate = request.EffectiveDate,
				OldGrossSalary = request.OldGrossSalary,
				NewGrossSalary = request.NewGrossSalary,
				IncrementAmount = request.IncrementAmount,
				IncrementPercent = request.IncrementPercent,
				ForwardedBy = command.ForwardedBy,
				ForwardDate = command.ForwardDate,
				IncrementType = request.IncrementType,
				IsActive = true
			};
			histories.Add(entity);
		}

		// Save PayIncrementHistory entities to the database
		var savedEntities = await _repository.AddRangeAsync(histories, cancellationToken);
		return savedEntities.Select(e => e.Id).ToList();
	}
}
