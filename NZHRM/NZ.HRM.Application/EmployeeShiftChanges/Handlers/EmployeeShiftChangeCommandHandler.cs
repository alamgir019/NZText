using NZ.HRM.Application.EmployeeShiftChanges.Commands.CreateEmployeeShiftChange;
using NZ.HRM.Application.EmployeeShiftChanges.Commands.DeleteEmployeeShiftChange;
using NZ.HRM.Application.EmployeeShiftChanges.Commands.GenerateAnnualShiftRotation;
using NZ.HRM.Application.EmployeeShiftChanges.Commands.GenerateJoinerShiftRotation;
using NZ.HRM.Application.EmployeeShiftChanges.Commands.UpdateEmployeeShiftChange;
using NZ.HRM.Application.EmployeeShiftChanges.DTOs;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.EmployeeShiftChanges.Handlers;

public class EmployeeShiftChangeCommandHandler
{
	private readonly IEmployeeShiftChangeRepository _shiftChangeRepository;
	private readonly IEmployeeEmploymentRepository _employmentRepository;
	private readonly IEmployeeMasterRepository _employeeMasterRepository;
	private readonly IShiftRepository _shiftRepository;

	public EmployeeShiftChangeCommandHandler(
		IEmployeeShiftChangeRepository shiftChangeRepository,
		IEmployeeEmploymentRepository employmentRepository,
		IEmployeeMasterRepository employeeMasterRepository,
		IShiftRepository shiftRepository)
	{
		_shiftChangeRepository = shiftChangeRepository;
		_employmentRepository = employmentRepository;
		_employeeMasterRepository = employeeMasterRepository;
		_shiftRepository = shiftRepository;
	}

	public async Task<string> Handle(
		CreateEmployeeShiftChangeCommand command,
		CancellationToken cancellationToken = default)
	{
		ValidateDateRange(command.EffectiveFrom, command.EffectiveTo);
		ValidateImmediateChange(command.EffectiveFrom, command.PreviousShiftId, command.NewShiftId);

		var employment = await _employmentRepository.GetByEmployeeIdAsync(command.EmployeeId, cancellationToken);
		if (employment == null)
			throw new KeyNotFoundException($"Employee with ID {command.EmployeeId} not found");

		await ValidateShiftsAsync(command.PreviousShiftId, command.NewShiftId, cancellationToken);

		if (!string.IsNullOrWhiteSpace(command.PreviousShiftId)
			&& employment.ShiftId != command.PreviousShiftId)
			throw new InvalidOperationException("The previous shift does not match the employee's current shift");

		var duplicateExists = await _shiftChangeRepository.ExistsActiveForEmployeeAndEffectivePeriodAsync(
			command.EmployeeId,
			command.EffectiveFrom,
			command.EffectiveTo,
			cancellationToken: cancellationToken);

		if (duplicateExists)
			throw new InvalidOperationException("An active shift change already exists for this employee and effective period");

		var overlapExists = await _shiftChangeRepository.ExistsActiveOverlappingPeriodAsync(
			command.EmployeeId,
			command.EffectiveFrom,
			command.EffectiveTo,
			cancellationToken: cancellationToken);

		if (overlapExists)
			throw new InvalidOperationException("An active shift change period overlaps with the requested effective period");

		var shiftChange = new HrmEmployeeShiftChange
		{
			EmployeeId = command.EmployeeId,
			PreviousShiftId = command.PreviousShiftId,
			NewShiftId = command.NewShiftId,
			EffectiveFrom = command.EffectiveFrom,
			EffectiveTo = command.EffectiveTo,
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
		ValidateDateRange(command.EffectiveFrom, command.EffectiveTo);
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

		var duplicateExists = await _shiftChangeRepository.ExistsActiveForEmployeeAndEffectivePeriodAsync(
			command.EmployeeId,
			command.EffectiveFrom,
			command.EffectiveTo,
			command.Id,
			cancellationToken);

		if (duplicateExists)
			throw new InvalidOperationException("An active shift change already exists for this employee and effective period");

		var overlapExists = await _shiftChangeRepository.ExistsActiveOverlappingPeriodAsync(
			command.EmployeeId,
			command.EffectiveFrom,
			command.EffectiveTo,
			command.Id,
			cancellationToken);

		if (overlapExists)
			throw new InvalidOperationException("An active shift change period overlaps with the requested effective period");

		shiftChange.PreviousShiftId = command.PreviousShiftId;
		shiftChange.NewShiftId = command.NewShiftId;
		shiftChange.EffectiveFrom = command.EffectiveFrom;
		shiftChange.EffectiveTo = command.EffectiveTo;
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

	public async Task<ShiftRotationGenerationResultDto> Handle(
		GenerateAnnualShiftRotationCommand command,
		CancellationToken cancellationToken = default)
	{
		if (command.Year < 2000 || command.Year > 9999)
			throw new ArgumentException("Invalid target year", nameof(command.Year));

		var rosterSequence = await GetRosterSequenceAsync(cancellationToken);
		var startOfYear = new DateOnly(command.Year, 1, 1);
		var endOfYear = new DateOnly(command.Year, 12, 31);

		if (startOfYear > endOfYear)
			return new ShiftRotationGenerationResultDto();

		var employees = await _employeeMasterRepository.GetAllAsync(
			includeInactive: false,
			includeEmployment: true,
			cancellationToken: cancellationToken);

		var eligibleEmployees = employees
			.Where(IsActiveRosterEmployee)
			.ToList();

		var result = new ShiftRotationGenerationResultDto
		{
			TotalEmployees = eligibleEmployees.Count
		};

		foreach (var employee in eligibleEmployees)
		{
			var item = new ShiftRotationGenerationResultItemDto { EmployeeId = employee.Id };

			if (employee.Employment?.ShiftId is null)
			{
				item.Succeeded = false;
				item.Message = "Employee has no assigned shift";
				result.Items.Add(item);
				continue;
			}

			var startIndex = rosterSequence.FindIndex(s => s.Id == employee.Employment.ShiftId);
			if (startIndex < 0)
			{
				item.Succeeded = false;
				item.Message = "Employee shift is not part of configured roster sequence";
				result.Items.Add(item);
				continue;
			}

			await _shiftChangeRepository.DeleteByEmployeeAndDateRangeAsync(
				employee.Id,
				startOfYear,
				endOfYear,
				cancellationToken);

			var records = GenerateAnnualRecords(employee.Id, startOfYear, endOfYear, rosterSequence, startIndex);
			await _shiftChangeRepository.AddRangeAsync(records, cancellationToken);

			item.Succeeded = true;
			item.GeneratedRecords = records.Count;
			result.Items.Add(item);
			result.ProcessedEmployees++;
			result.GeneratedRecords += records.Count;
		}

		result.SkippedEmployees = result.TotalEmployees - result.ProcessedEmployees;
		return result;
	}

	public async Task<ShiftRotationGenerationResultDto> Handle(
		GenerateJoinerShiftRotationCommand command,
		CancellationToken cancellationToken = default)
	{
		var year = command.Year ?? DateTime.UtcNow.Year;
		if (year < 2000 || year > 9999)
			throw new ArgumentException("Invalid target year", nameof(command.Year));

		var employee = await _employeeMasterRepository.GetByIdAsync(command.EmployeeId, cancellationToken);
		if (employee == null)
			throw new KeyNotFoundException($"Employee with ID {command.EmployeeId} not found");

		if (!IsActiveRosterEmployee(employee))
			throw new InvalidOperationException("Only active roster employees are eligible for join-date rotation generation");

		var joiningDate = employee.Employment?.JoiningDate;
		if (!joiningDate.HasValue)
			throw new InvalidOperationException("Join date is required to generate shift rotations");

		var endOfYear = new DateOnly(year, 12, 31);
		if (joiningDate.Value > endOfYear)
			throw new InvalidOperationException("Join date cannot be after December 31 of the target year");

		if (employee.Employment?.ShiftId is null)
			throw new InvalidOperationException("Employee has no assigned shift");

		var rosterSequence = await GetRosterSequenceAsync(cancellationToken);
		var startIndex = rosterSequence.FindIndex(s => s.Id == employee.Employment.ShiftId);
		if (startIndex < 0)
			throw new InvalidOperationException("Employee shift is not part of configured roster sequence");

		var records = GenerateJoinerRecords(employee.Id, joiningDate.Value, endOfYear, rosterSequence, startIndex);
		var validRecords = new List<HrmEmployeeShiftChange>();

		foreach (var record in records)
		{
			var exactExists = await _shiftChangeRepository.ExistsActiveForEmployeeAndEffectivePeriodAsync(
				record.EmployeeId,
				record.EffectiveFrom,
				record.EffectiveTo,
				cancellationToken: cancellationToken);

			if (exactExists)
				continue;

			var overlapExists = await _shiftChangeRepository.ExistsActiveOverlappingPeriodAsync(
				record.EmployeeId,
				record.EffectiveFrom,
				record.EffectiveTo,
				cancellationToken: cancellationToken);

			if (overlapExists)
				throw new InvalidOperationException("Existing shift changes overlap with the join-date generation period");

			validRecords.Add(record);
		}

		await _shiftChangeRepository.AddRangeAsync(validRecords, cancellationToken);

		return new ShiftRotationGenerationResultDto
		{
			TotalEmployees = 1,
			ProcessedEmployees = 1,
			GeneratedRecords = validRecords.Count,
			Items =
			[
				new ShiftRotationGenerationResultItemDto
				{
					EmployeeId = employee.Id,
					Succeeded = true,
					GeneratedRecords = validRecords.Count
				}
			]
		};
	}

	private async Task ValidateShiftsAsync(
		string? previousShiftId,
		string newShiftId,
		CancellationToken cancellationToken)
	{
		if (!string.IsNullOrWhiteSpace(previousShiftId))
		{
			var previousShift = await _shiftRepository.GetByIdAsync(previousShiftId, cancellationToken);
			if (previousShift == null)
				throw new KeyNotFoundException($"Previous shift with ID {previousShiftId} not found");
		}

		var newShift = await _shiftRepository.GetByIdAsync(newShiftId, cancellationToken);
		if (newShift == null)
			throw new KeyNotFoundException($"New shift with ID {newShiftId} not found");
	}

	private static void ValidateImmediateChange(
		DateOnly effectiveFrom,
		string? previousShiftId,
		string newShiftId)
	{
		if (effectiveFrom > DateOnly.FromDateTime(DateTime.UtcNow))
			throw new ArgumentException("Effective date cannot be in the future", nameof(effectiveFrom));

		if (!string.IsNullOrWhiteSpace(previousShiftId)
			&& string.Equals(previousShiftId, newShiftId, StringComparison.Ordinal))
			throw new ArgumentException("Previous and new shifts must be different");
	}

	private static void ValidateDateRange(DateOnly effectiveFrom, DateOnly effectiveTo)
	{
		if (effectiveFrom > effectiveTo)
			throw new ArgumentException("EffectiveFrom must be less than or equal to EffectiveTo");
	}

	private static bool IsActiveRosterEmployee(HrmEmployeeMaster employee)
	{
		if (!employee.IsActive)
			return false;

		if (!string.Equals(employee.Status, EmployeeStatus.ITActivation.ToString(), StringComparison.OrdinalIgnoreCase))
			return false;

		var shiftType = employee.Employment?.Shift?.ShiftType;
		return string.Equals(shiftType, "Roaster", StringComparison.OrdinalIgnoreCase)
			|| string.Equals(shiftType, "Roster", StringComparison.OrdinalIgnoreCase);
	}

	private async Task<List<MstShift>> GetRosterSequenceAsync(CancellationToken cancellationToken)
	{
		var shifts = await _shiftRepository.GetAllAsync(cancellationToken: cancellationToken);
		var rosterShifts = shifts
			.Where(s => string.Equals(s.ShiftType, "Roaster", StringComparison.OrdinalIgnoreCase)
				|| string.Equals(s.ShiftType, "Roster", StringComparison.OrdinalIgnoreCase))
			.ToList();

		var requiredCodes = new[] { "A", "B", "C" };
		var sequence = requiredCodes
			.Select(code => rosterShifts.FirstOrDefault(s => string.Equals(s.ShiftCode, code, StringComparison.OrdinalIgnoreCase)))
			.ToList();

		if (sequence.Any(s => s == null))
			throw new InvalidOperationException("Configured roster shift sequence A -> B -> C is missing");

		return sequence!;
	}

	private static List<HrmEmployeeShiftChange> GenerateAnnualRecords(
		string employeeId,
		DateOnly startDate,
		DateOnly endDate,
		List<MstShift> sequence,
		int startShiftIndex)
	{
		var records = new List<HrmEmployeeShiftChange>();
		var periodStart = startDate;
		var index = startShiftIndex;

		while (periodStart <= endDate)
		{
			var periodEnd = periodStart.AddDays(6);
			if (periodEnd > endDate)
				periodEnd = endDate;

			var newShiftId = sequence[index].Id;
			var previousShiftId = records.Count == 0 ? null : sequence[(index - 1 + sequence.Count) % sequence.Count].Id;

			records.Add(new HrmEmployeeShiftChange
			{
				EmployeeId = employeeId,
				PreviousShiftId = previousShiftId,
				NewShiftId = newShiftId,
				EffectiveFrom = periodStart,
				EffectiveTo = periodEnd,
				IsActive = true
			});

			periodStart = periodEnd.AddDays(1);
			index = (index + 1) % sequence.Count;
		}

		return records;
	}

	private static List<HrmEmployeeShiftChange> GenerateJoinerRecords(
		string employeeId,
		DateOnly joiningDate,
		DateOnly endDate,
		List<MstShift> sequence,
		int startShiftIndex)
	{
		var records = new List<HrmEmployeeShiftChange>();
		if (joiningDate > endDate)
			return records;

		var firstEnd = GetUpcomingFriday(joiningDate);
		if (firstEnd > endDate)
			firstEnd = endDate;

		records.Add(new HrmEmployeeShiftChange
		{
			EmployeeId = employeeId,
			PreviousShiftId = sequence[(startShiftIndex - 1 + sequence.Count) % sequence.Count].Id,
			NewShiftId = sequence[startShiftIndex].Id,
			EffectiveFrom = joiningDate,
			EffectiveTo = firstEnd,
			IsActive = true
		});

		var periodStart = firstEnd.AddDays(1);
		var index = (startShiftIndex + 1) % sequence.Count;

		while (periodStart <= endDate)
		{
			var periodEnd = periodStart.AddDays(6);
			if (periodEnd > endDate)
				periodEnd = endDate;

			records.Add(new HrmEmployeeShiftChange
			{
				EmployeeId = employeeId,
				PreviousShiftId = sequence[(index - 1 + sequence.Count) % sequence.Count].Id,
				NewShiftId = sequence[index].Id,
				EffectiveFrom = periodStart,
				EffectiveTo = periodEnd,
				IsActive = true
			});

			periodStart = periodEnd.AddDays(1);
			index = (index + 1) % sequence.Count;
		}

		return records;
	}

	private static DateOnly GetUpcomingFriday(DateOnly fromDate)
	{
		var offset = ((int)DayOfWeek.Friday - (int)fromDate.DayOfWeek + 7) % 7;
		return fromDate.AddDays(offset);
	}
}
