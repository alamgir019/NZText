using NZ.HRM.Application.EmployeeShiftChanges.Commands.CreateEmployeeShiftChange;
using NZ.HRM.Application.EmployeeShiftChanges.Commands.DeleteEmployeeShiftChange;
using NZ.HRM.Application.EmployeeShiftChanges.Commands.UpdateEmployeeShiftChange;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.EmployeeShiftChanges.Handlers;

public class EmployeeShiftChangeCommandHandler
{
	private readonly IEmployeeShiftChangeRepository _shiftChangeRepository;
	private readonly IEmployeeEmploymentRepository _employmentRepository;
	private readonly IShiftRepository _shiftRepository;

	public EmployeeShiftChangeCommandHandler(
		IEmployeeShiftChangeRepository shiftChangeRepository,
		IEmployeeEmploymentRepository employmentRepository,
		IShiftRepository shiftRepository)
	{
		_shiftChangeRepository = shiftChangeRepository;
		_employmentRepository = employmentRepository;
		_shiftRepository = shiftRepository;
	}

	public async Task<string> Handle(
		CreateEmployeeShiftChangeCommand command,
		CancellationToken cancellationToken = default)
	{
		ValidateImmediateChange(command.EffectiveFrom, command.PreviousShiftId, command.NewShiftId);

		var employment = await _employmentRepository.GetByEmployeeIdAsync(command.EmployeeId, cancellationToken);
		if (employment == null)
			throw new KeyNotFoundException($"Employee with ID {command.EmployeeId} not found");

		await ValidateShiftsAsync(command.PreviousShiftId, command.NewShiftId, cancellationToken);

		if (employment.ShiftId != command.PreviousShiftId)
			throw new InvalidOperationException("The previous shift does not match the employee's current shift");

		var duplicateExists = await _shiftChangeRepository.ExistsActiveForEmployeeAndEffectiveDateAsync(
			command.EmployeeId,
			command.EffectiveFrom,
			cancellationToken: cancellationToken);

		if (duplicateExists)
			throw new InvalidOperationException("An active shift change already exists for this employee and effective date");

		var shiftChange = new HrmEmployeeShiftChange
		{
			EmployeeId = command.EmployeeId,
			PreviousShiftId = command.PreviousShiftId,
			NewShiftId = command.NewShiftId,
			EffectiveFrom = command.EffectiveFrom,
			Remarks = command.Remarks,
			IsActive = true
		};

		employment.ShiftId = command.NewShiftId;

		return await _shiftChangeRepository.AddWithEmploymentAsync(
			shiftChange,
			employment,
			cancellationToken);
	}

	public async Task Handle(
		UpdateEmployeeShiftChangeCommand command,
		CancellationToken cancellationToken = default)
	{
		ValidateImmediateChange(command.EffectiveFrom, command.PreviousShiftId, command.NewShiftId);

		var shiftChange = await _shiftChangeRepository.GetByIdAsync(command.Id, cancellationToken);
		if (shiftChange == null)
			throw new KeyNotFoundException($"Employee shift change with ID {command.Id} not found");

		if (shiftChange.EmployeeId != command.EmployeeId)
			throw new InvalidOperationException("The employee cannot be changed for an existing shift change");

		var latestChange = await _shiftChangeRepository.GetLatestByEmployeeIdAsync(
			shiftChange.EmployeeId,
			cancellationToken);

		if (latestChange == null || latestChange.Id != shiftChange.Id)
			throw new InvalidOperationException("Only the latest active shift change can be updated");

		var employment = await _employmentRepository.GetByEmployeeIdAsync(command.EmployeeId, cancellationToken);
		if (employment == null)
			throw new KeyNotFoundException($"Employee with ID {command.EmployeeId} not found");

		await ValidateShiftsAsync(command.PreviousShiftId, command.NewShiftId, cancellationToken);

		if (employment.ShiftId != shiftChange.NewShiftId)
			throw new InvalidOperationException("The employee's current shift does not match the existing shift change");

		var duplicateExists = await _shiftChangeRepository.ExistsActiveForEmployeeAndEffectiveDateAsync(
			command.EmployeeId,
			command.EffectiveFrom,
			command.Id,
			cancellationToken);

		if (duplicateExists)
			throw new InvalidOperationException("An active shift change already exists for this employee and effective date");

		shiftChange.PreviousShiftId = command.PreviousShiftId;
		shiftChange.NewShiftId = command.NewShiftId;
		shiftChange.EffectiveFrom = command.EffectiveFrom;
		shiftChange.Remarks = command.Remarks;
		employment.ShiftId = command.NewShiftId;

		await _shiftChangeRepository.UpdateWithEmploymentAsync(
			shiftChange,
			employment,
			cancellationToken);
	}

	public async Task Handle(
		DeleteEmployeeShiftChangeCommand command,
		CancellationToken cancellationToken = default)
	{
		var shiftChange = await _shiftChangeRepository.GetByIdAsync(command.Id, cancellationToken);
		if (shiftChange == null)
			throw new KeyNotFoundException($"Employee shift change with ID {command.Id} not found");

		var latestChange = await _shiftChangeRepository.GetLatestByEmployeeIdAsync(
			shiftChange.EmployeeId,
			cancellationToken);

		if (latestChange == null || latestChange.Id != shiftChange.Id)
			throw new InvalidOperationException("Only the latest active shift change can be deleted");

		var employment = await _employmentRepository.GetByEmployeeIdAsync(
			shiftChange.EmployeeId,
			cancellationToken);

		if (employment == null)
			throw new KeyNotFoundException($"Employee with ID {shiftChange.EmployeeId} not found");

		if (employment.ShiftId != shiftChange.NewShiftId)
			throw new InvalidOperationException("The employee's current shift does not match the shift change");

		// Soft delete the shift change and revert the employee's shift to the previous shift
		shiftChange.IsActive = false;
		employment.ShiftId = shiftChange.PreviousShiftId;

		await _shiftChangeRepository.DeleteWithEmploymentAsync(
			shiftChange,
			employment,
			cancellationToken);
	}

	private async Task ValidateShiftsAsync(
		string previousShiftId,
		string newShiftId,
		CancellationToken cancellationToken)
	{
		var previousShift = await _shiftRepository.GetByIdAsync(previousShiftId, cancellationToken);
		if (previousShift == null)
			throw new KeyNotFoundException($"Previous shift with ID {previousShiftId} not found");

		var newShift = await _shiftRepository.GetByIdAsync(newShiftId, cancellationToken);
		if (newShift == null)
			throw new KeyNotFoundException($"New shift with ID {newShiftId} not found");
	}

	private static void ValidateImmediateChange(
		DateOnly effectiveFrom,
		string previousShiftId,
		string newShiftId)
	{
		if (effectiveFrom > DateOnly.FromDateTime(DateTime.UtcNow))
			throw new ArgumentException("Effective date cannot be in the future", nameof(effectiveFrom));

		if (string.Equals(previousShiftId, newShiftId, StringComparison.Ordinal))
			throw new ArgumentException("Previous and new shifts must be different");
	}
}
