using NZ.HRM.Application.EmployeeTransfers.Queries.GetAllEmployeeTransfers;
using NZ.HRM.Application.EmployeeTransfers.Queries.GetEmployeeTransferById;
using NZ.HRM.Application.EmployeeTransfers.Queries.GetEmployeeTransfersByEmployee;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.EmployeeTransfers.Handlers;

public class EmployeeTransferQueryHandler
{
	private readonly IEmployeeTransferRepository _repository;

	public EmployeeTransferQueryHandler(IEmployeeTransferRepository repository)
	{
		_repository = repository;
	}

	public async Task<List<EmployeeTransferDto>> Handle(
		GetAllEmployeeTransfersQuery query,
		CancellationToken cancellationToken = default)
	{
		var transfers = await _repository.GetAllAsync(
			query.IncludeInactive,
			query.EmployeeId,
			query.FromDate,
			query.ToDate,
			cancellationToken);

		return transfers.Select(MapToDto).ToList();
	}

	public async Task<EmployeeTransferDetailDto?> Handle(
		GetEmployeeTransferByIdQuery query,
		CancellationToken cancellationToken = default)
	{
		var transfer = await _repository.GetByIdAsync(query.Id, cancellationToken);
		return transfer == null ? null : MapToDetailDto(transfer);
	}

	public async Task<List<EmployeeTransferDto>> Handle(
		GetEmployeeTransfersByEmployeeQuery query,
		CancellationToken cancellationToken = default)
	{
		var transfers = await _repository.GetByEmployeeIdAsync(
			query.EmployeeId,
			query.IncludeInactive,
			query.FromDate,
			query.ToDate,
			cancellationToken);

		return transfers.Select(MapToDto).ToList();
	}

	private static EmployeeTransferDto MapToDto(HrmEmployeeTransfer transfer)
	{
		return new EmployeeTransferDto
		{
			Id = transfer.Id,
			EmployeeId = transfer.EmployeeId,
			EmployeeCode = transfer.Employee?.EmployeeCode ?? string.Empty,
			EmployeeName = transfer.Employee?.EmployeeName ?? string.Empty,
			PreviousDepartmentId = transfer.PreviousDepartmentId,
			PreviousDepartmentName = transfer.PreviousDepartment?.DepartmentName ?? string.Empty,
			PreviousSectionId = transfer.PreviousSectionId,
			PreviousSectionName = transfer.PreviousSection?.SectionName ?? string.Empty,
			NewDepartmentId = transfer.NewDepartmentId,
			NewDepartmentName = transfer.NewDepartment?.DepartmentName ?? string.Empty,
			NewSectionId = transfer.NewSectionId,
			NewSectionName = transfer.NewSection?.SectionName ?? string.Empty,
			EffectiveFrom = transfer.EffectiveFrom,
			Remarks = transfer.Remarks,
			IsActive = transfer.IsActive
		};
	}

	private static EmployeeTransferDetailDto MapToDetailDto(HrmEmployeeTransfer transfer)
	{
		return new EmployeeTransferDetailDto
		{
			Id = transfer.Id,
			EmployeeId = transfer.EmployeeId,
			EmployeeCode = transfer.Employee?.EmployeeCode ?? string.Empty,
			EmployeeName = transfer.Employee?.EmployeeName ?? string.Empty,
			PreviousDepartmentId = transfer.PreviousDepartmentId,
			PreviousDepartmentName = transfer.PreviousDepartment?.DepartmentName ?? string.Empty,
			PreviousSectionId = transfer.PreviousSectionId,
			PreviousSectionName = transfer.PreviousSection?.SectionName ?? string.Empty,
			NewDepartmentId = transfer.NewDepartmentId,
			NewDepartmentName = transfer.NewDepartment?.DepartmentName ?? string.Empty,
			NewSectionId = transfer.NewSectionId,
			NewSectionName = transfer.NewSection?.SectionName ?? string.Empty,
			EffectiveFrom = transfer.EffectiveFrom,
			Remarks = transfer.Remarks,
			CreatedOn = transfer.CreatedOn,
			CreatedBy = transfer.CreatedBy,
			UpdatedOn = transfer.UpdatedOn,
			UpdatedBy = transfer.UpdatedBy,
			IsActive = transfer.IsActive
		};
	}
}
