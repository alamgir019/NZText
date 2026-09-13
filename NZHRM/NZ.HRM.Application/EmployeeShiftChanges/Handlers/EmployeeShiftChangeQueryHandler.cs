using NZ.HRM.Application.EmployeeShiftChanges.Queries.GetAllEmployeeShiftChanges;
using NZ.HRM.Application.EmployeeShiftChanges.Queries.GetEmployeeShiftChangeById;
using NZ.HRM.Application.EmployeeShiftChanges.Queries.GetEmployeeShiftChangesByEmployee;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.EmployeeShiftChanges.Handlers;

public class EmployeeShiftChangeQueryHandler
{
	private readonly IEmployeeShiftChangeRepository _repository;

	public EmployeeShiftChangeQueryHandler(IEmployeeShiftChangeRepository repository)
	{
		_repository = repository;
	}

	public async Task<List<EmployeeShiftChangeDto>> Handle(
		GetAllEmployeeShiftChangesQuery query,
		CancellationToken cancellationToken = default)
	{
		var changes = await _repository.GetAllAsync(
			query.IncludeInactive,
			query.EmployeeId,
			query.FromDate,
			query.ToDate,
			cancellationToken);

		return changes.Select(MapToDto).ToList();
	}

	public async Task<EmployeeShiftChangeDetailDto?> Handle(
		GetEmployeeShiftChangeByIdQuery query,
		CancellationToken cancellationToken = default)
	{
		var change = await _repository.GetByIdAsync(query.Id, cancellationToken);
		return change == null ? null : MapToDetailDto(change);
	}

	public async Task<List<EmployeeShiftChangeDto>> Handle(
		GetEmployeeShiftChangesByEmployeeQuery query,
		CancellationToken cancellationToken = default)
	{
		var changes = await _repository.GetByEmployeeIdAsync(
			query.EmployeeId,
			query.IncludeInactive,
			query.FromDate,
			query.ToDate,
			cancellationToken);

		return changes.Select(MapToDto).ToList();
	}

	private static EmployeeShiftChangeDto MapToDto(HrmEmployeeShiftChange change)
	{
		return new EmployeeShiftChangeDto
		{
			Id = change.Id,
			EmployeeId = change.EmployeeId,
			EmployeeName = change.Employee?.EmployeeName ?? string.Empty,
			PreviousShiftId = change.PreviousShiftId,
			PreviousShiftName = change.PreviousShift?.ShiftName ?? string.Empty,
			NewShiftId = change.NewShiftId,
			NewShiftName = change.NewShift?.ShiftName ?? string.Empty,
			EffectiveFrom = change.EffectiveFrom,
			Remarks = change.Remarks,
			IsActive = change.IsActive
		};
	}

	private static EmployeeShiftChangeDetailDto MapToDetailDto(HrmEmployeeShiftChange change)
	{
		return new EmployeeShiftChangeDetailDto
		{
			Id = change.Id,
			EmployeeId = change.EmployeeId,
			EmployeeName = change.Employee?.EmployeeName ?? string.Empty,
			PreviousShiftId = change.PreviousShiftId,
			PreviousShiftName = change.PreviousShift?.ShiftName ?? string.Empty,
			NewShiftId = change.NewShiftId,
			NewShiftName = change.NewShift?.ShiftName ?? string.Empty,
			EffectiveFrom = change.EffectiveFrom,
			Remarks = change.Remarks,
			CreatedOn = change.CreatedOn,
			CreatedBy = change.CreatedBy,
			UpdatedOn = change.UpdatedOn,
			UpdatedBy = change.UpdatedBy,
			IsActive = change.IsActive
		};
	}
}
