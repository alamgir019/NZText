using NZ.HRM.Application.EmployeeTransfers.Commands.CreateEmployeeTransfer;
using NZ.HRM.Application.EmployeeTransfers.Commands.DeleteEmployeeTransfer;
using NZ.HRM.Application.EmployeeTransfers.Commands.UpdateEmployeeTransfer;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.EmployeeTransfers.Handlers;

public class EmployeeTransferCommandHandler
{
	private readonly IEmployeeTransferRepository _transferRepository;
	private readonly IEmployeeEmploymentRepository _employmentRepository;
	private readonly IDepartmentRepository _departmentRepository;
	private readonly ISectionRepository _sectionRepository;

	public EmployeeTransferCommandHandler(
		IEmployeeTransferRepository transferRepository,
		IEmployeeEmploymentRepository employmentRepository,
		IDepartmentRepository departmentRepository,
		ISectionRepository sectionRepository)
	{
		_transferRepository = transferRepository;
		_employmentRepository = employmentRepository;
		_departmentRepository = departmentRepository;
		_sectionRepository = sectionRepository;
	}

	public async Task<string> Handle(
		CreateEmployeeTransferCommand command,
		CancellationToken cancellationToken = default)
	{
		ValidateImmediateTransfer(command.EffectiveFrom, command.PreviousDepartmentId, command.PreviousSectionId, command.NewDepartmentId, command.NewSectionId);

		var employment = await _employmentRepository.GetByEmployeeIdAsync(command.EmployeeId, cancellationToken);
		
		if (employment == null)
			throw new KeyNotFoundException($"Employee with ID {command.EmployeeId} not found");

		ValidateCurrentEmployment( employment, command.PreviousDepartmentId, command.PreviousSectionId);

		await ValidateTransferReferencesAsync(
			command.PreviousDepartmentId,
			command.PreviousSectionId,
			command.NewDepartmentId,
			command.NewSectionId,
			cancellationToken);

		var duplicateExists = await _transferRepository.ExistsActiveForEmployeeAndEffectiveDateAsync(
			command.EmployeeId,
			command.EffectiveFrom,
			cancellationToken: cancellationToken);

		if (duplicateExists)
			throw new InvalidOperationException("An active employee transfer already exists for this employee and effective date");

		var transfer = new HrmEmployeeTransfer
		{
			EmployeeId = command.EmployeeId,
			PreviousDepartmentId = command.PreviousDepartmentId,
			PreviousSectionId = command.PreviousSectionId,
			NewDepartmentId = command.NewDepartmentId,
			NewSectionId = command.NewSectionId,
			EffectiveFrom = command.EffectiveFrom,
			Remarks = command.Remarks,
			IsActive = true
		};

		employment.DepartmentId = command.NewDepartmentId;
		employment.SectionId = command.NewSectionId;

		return await _transferRepository.AddWithEmploymentAsync(transfer, employment, cancellationToken);
	}

	public async Task Handle(
		UpdateEmployeeTransferCommand command,
		CancellationToken cancellationToken = default)
	{
		ValidateImmediateTransfer(
			command.EffectiveFrom,
			command.PreviousDepartmentId,
			command.PreviousSectionId,
			command.NewDepartmentId,
			command.NewSectionId);

		var transfer = await _transferRepository.GetByIdAsync(command.Id, cancellationToken);
		if (transfer == null)
			throw new KeyNotFoundException($"Employee transfer with ID {command.Id} not found");

		if (transfer.EmployeeId != command.EmployeeId)
			throw new InvalidOperationException("The employee cannot be changed for an existing transfer");

		var latestTransfer = await _transferRepository.GetLatestByEmployeeIdAsync(
			transfer.EmployeeId,
			cancellationToken);
		if (latestTransfer == null || latestTransfer.Id != transfer.Id)
			throw new InvalidOperationException("Only the latest active employee transfer can be updated");

		var employment = await _employmentRepository.GetByEmployeeIdAsync(
			command.EmployeeId,
			cancellationToken);
		if (employment == null)
			throw new KeyNotFoundException($"Employee with ID {command.EmployeeId} not found");

		ValidateCurrentEmployment(
			employment,
			transfer.NewDepartmentId,
			transfer.NewSectionId);

		await ValidateTransferReferencesAsync(
			command.PreviousDepartmentId,
			command.PreviousSectionId,
			command.NewDepartmentId,
			command.NewSectionId,
			cancellationToken);

		var duplicateExists = await _transferRepository.ExistsActiveForEmployeeAndEffectiveDateAsync(
			command.EmployeeId,
			command.EffectiveFrom,
			command.Id,
			cancellationToken);
		if (duplicateExists)
			throw new InvalidOperationException("An active employee transfer already exists for this employee and effective date");

		transfer.PreviousDepartmentId = command.PreviousDepartmentId;
		transfer.PreviousSectionId = command.PreviousSectionId;
		transfer.NewDepartmentId = command.NewDepartmentId;
		transfer.NewSectionId = command.NewSectionId;
		transfer.EffectiveFrom = command.EffectiveFrom;
		transfer.Remarks = command.Remarks;
		employment.DepartmentId = command.NewDepartmentId;
		employment.SectionId = command.NewSectionId;

		await _transferRepository.UpdateWithEmploymentAsync(
			transfer,
			employment,
			cancellationToken);
	}

	public async Task Handle(
		DeleteEmployeeTransferCommand command,
		CancellationToken cancellationToken = default)
	{
		var transfer = await _transferRepository.GetByIdAsync(command.Id, cancellationToken);
		if (transfer == null)
			throw new KeyNotFoundException($"Employee transfer with ID {command.Id} not found");

		var latestTransfer = await _transferRepository.GetLatestByEmployeeIdAsync(
			transfer.EmployeeId,
			cancellationToken);
		if (latestTransfer == null || latestTransfer.Id != transfer.Id)
			throw new InvalidOperationException("Only the latest active employee transfer can be deleted");

		var employment = await _employmentRepository.GetByEmployeeIdAsync(
			transfer.EmployeeId,
			cancellationToken);
		if (employment == null)
			throw new KeyNotFoundException($"Employee with ID {transfer.EmployeeId} not found");

		ValidateCurrentEmployment(
			employment,
			transfer.NewDepartmentId,
			transfer.NewSectionId);

		// Mark the transfer as inactive and revert the employment to the previous department and section
		transfer.IsActive = false;
		employment.DepartmentId = transfer.PreviousDepartmentId;
		employment.SectionId = transfer.PreviousSectionId;

		await _transferRepository.DeleteWithEmploymentAsync(
			transfer,
			employment,
			cancellationToken);
	}

	// Validates that the provided department and section IDs exist and are correctly related.
	private async Task ValidateTransferReferencesAsync(
		string previousDepartmentId,
		string previousSectionId,
		string newDepartmentId,
		string newSectionId,
		CancellationToken cancellationToken)
	{
		var previousDepartment = await _departmentRepository.GetByIdAsync(
			previousDepartmentId,
			cancellationToken);
		if (previousDepartment == null)
			throw new KeyNotFoundException($"Previous department with ID {previousDepartmentId} not found");

		var newDepartment = await _departmentRepository.GetByIdAsync(
			newDepartmentId,
			cancellationToken);
		if (newDepartment == null)
			throw new KeyNotFoundException($"New department with ID {newDepartmentId} not found");

		var previousSection = await _sectionRepository.GetByIdAsync(
			previousSectionId,
			cancellationToken);
		if (previousSection == null)
			throw new KeyNotFoundException($"Previous section with ID {previousSectionId} not found");

		var newSection = await _sectionRepository.GetByIdAsync(
			newSectionId,
			cancellationToken);
		if (newSection == null)
			throw new KeyNotFoundException($"New section with ID {newSectionId} not found");

		if (previousSection.DepartmentId != previousDepartmentId)
			throw new InvalidOperationException("The previous section does not belong to the previous department");

		if (newSection.DepartmentId != newDepartmentId)
			throw new InvalidOperationException("The new section does not belong to the new department");
	}

	// Validates that the employee's current department and section match the provided department and section IDs.
	private static void ValidateCurrentEmployment(
		HrmEmployeeEmployment employment,
		string departmentId,
		string sectionId)
	{
		if (employment.DepartmentId != departmentId || employment.SectionId != sectionId)
			throw new InvalidOperationException("The employee's current department and section do not match the transfer source");
	}

	// Validates that the effective date is not in the future and that the previous and new department and section IDs are valid and different.
	private static void ValidateImmediateTransfer(
		DateOnly effectiveFrom,
		string previousDepartmentId,
		string previousSectionId,
		string newDepartmentId,
		string newSectionId)
	{
		if (effectiveFrom > DateOnly.FromDateTime(DateTime.UtcNow))
			throw new ArgumentException("Effective date cannot be in the future", nameof(effectiveFrom));

		if (string.IsNullOrWhiteSpace(previousDepartmentId)
			|| string.IsNullOrWhiteSpace(previousSectionId)
			|| string.IsNullOrWhiteSpace(newDepartmentId)
			|| string.IsNullOrWhiteSpace(newSectionId))
			throw new ArgumentException("Department and section IDs are required");

		if (string.Equals(previousDepartmentId, newDepartmentId, StringComparison.Ordinal)
			&& string.Equals(previousSectionId, newSectionId, StringComparison.Ordinal))
			throw new ArgumentException("Previous and new department and section must be different");
	}
}
