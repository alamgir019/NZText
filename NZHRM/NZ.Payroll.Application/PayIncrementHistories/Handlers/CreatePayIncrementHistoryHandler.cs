using NZ.HRM.Domain.Entities;
using NZ.Payroll.Application.Interfaces.Repositories;
using NZ.Payroll.Application.PayIncrementHistories.Commands;
using NZ.Payroll.Application.PayIncrementHistories.DTOs;
using NZ.Shared.Contracts.HRM;

namespace NZ.Payroll.Application.PayIncrementHistories.Handlers;

public class CreatePayIncrementHistoryHandler
{
	private readonly IPayIncrementHistoryRepository _repository;
	private readonly IEmployeeQuery _employeeQuery;

	public CreatePayIncrementHistoryHandler(IPayIncrementHistoryRepository repository, IEmployeeQuery employeeQuery)
	{
		_repository = repository;
		_employeeQuery = employeeQuery;
	}

	public async Task<String> Handle(CreatePayIncrementHistoryCommand command, CancellationToken cancellationToken = default)
	{
		

		if (string.IsNullOrWhiteSpace(command.EmployeeId))
			throw new ArgumentException("Employee ID is required", nameof(command.EmployeeId));

		var employee = await _employeeQuery.GetByIdAsync(command.EmployeeId, cancellationToken);
		if (employee is null)
			throw new KeyNotFoundException($"Employee with ID '{command.EmployeeId}' not found");

		var duplicateExists = await _repository.ExistsByEmployeeAndEffectiveDateAsync(command.EmployeeId, command.EffectiveDate, cancellationToken);
		if (duplicateExists)
			throw new InvalidOperationException($"An increment history record already exists for employee '{command.EmployeeId}' with effective date '{command.EffectiveDate}'");

		var entity = new PayIncrementHistory
		{
			EmployeeId = command.EmployeeId,
			EffectiveDate = command.EffectiveDate,
			OldGrossSalary = command.OldGrossSalary,
			NewGrossSalary = command.NewGrossSalary,
			IncrementAmount = command.IncrementAmount,
			IncrementPercent = command.IncrementPercent,
			ApprovedBy = command.ApprovedBy,
			ApprovalDate = command.ApprovalDate,
			ForwardedBy = command.ForwardedBy,
			ForwardDate = command.ForwardDate,
			IncrementType = command.IncrementType,
			IsActive = true
		};


		// Save PayIncrementHistory entity to the database
		var savedEntity = await _repository.AddAsync(entity, cancellationToken);
		return savedEntity.Id;
	}
}
