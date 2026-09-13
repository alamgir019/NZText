using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class EmployeeTransferRepository : IEmployeeTransferRepository
{
	private readonly ApplicationDbContext _context;

	public EmployeeTransferRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<List<HrmEmployeeTransfer>> GetAllAsync(
		bool includeInactive = false,
		string? employeeId = null,
		DateOnly? fromDate = null,
		DateOnly? toDate = null,
		CancellationToken cancellationToken = default)
	{
		var query = BuildQuery(includeInactive)
			.AsQueryable();

		if (!string.IsNullOrWhiteSpace(employeeId))
			query = query.Where(x => x.EmployeeId == employeeId);

		if (fromDate.HasValue)
			query = query.Where(x => x.EffectiveFrom >= fromDate.Value);

		if (toDate.HasValue)
			query = query.Where(x => x.EffectiveFrom <= toDate.Value);

		return await query
			.OrderByDescending(x => x.EffectiveFrom)
			.ThenByDescending(x => x.CreatedOn)
			.ToListAsync(cancellationToken);
	}

	public async Task<HrmEmployeeTransfer?> GetByIdAsync(
		string id,
		CancellationToken cancellationToken = default)
	{
		return await BuildQuery(false)
			.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
	}

	public async Task<List<HrmEmployeeTransfer>> GetByEmployeeIdAsync(
		string employeeId,
		bool includeInactive = false,
		DateOnly? fromDate = null,
		DateOnly? toDate = null,
		CancellationToken cancellationToken = default)
	{
		return await GetAllAsync(includeInactive, employeeId, fromDate, toDate, cancellationToken);
	}

	public async Task<HrmEmployeeTransfer?> GetLatestByEmployeeIdAsync(
		string employeeId,
		CancellationToken cancellationToken = default)
	{
		return await BuildQuery(false)
			.Where(x => x.EmployeeId == employeeId)
			.OrderByDescending(x => x.EffectiveFrom)
			.ThenByDescending(x => x.CreatedOn)
			.FirstOrDefaultAsync(cancellationToken);
	}

	public async Task<string> AddWithEmploymentAsync(
		HrmEmployeeTransfer transfer,
		HrmEmployeeEmployment employment,
		CancellationToken cancellationToken = default)
	{
		await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

		_context.HrmEmployeeTransfers.Add(transfer);
		_context.HrmEmployeeEmployments.Update(employment);
		await _context.SaveChangesAsync(cancellationToken);
		await transaction.CommitAsync(cancellationToken);

		return transfer.Id;
	}

	public async Task UpdateWithEmploymentAsync(
		HrmEmployeeTransfer transfer,
		HrmEmployeeEmployment employment,
		CancellationToken cancellationToken = default)
	{
		await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

		_context.HrmEmployeeTransfers.Update(transfer);
		_context.HrmEmployeeEmployments.Update(employment);
		await _context.SaveChangesAsync(cancellationToken);
		await transaction.CommitAsync(cancellationToken);
	}

	public async Task DeleteWithEmploymentAsync(
		HrmEmployeeTransfer transfer,
		HrmEmployeeEmployment employment,
		CancellationToken cancellationToken = default)
	{
		await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

		_context.HrmEmployeeTransfers.Update(transfer);
		_context.HrmEmployeeEmployments.Update(employment);
		await _context.SaveChangesAsync(cancellationToken);
		await transaction.CommitAsync(cancellationToken);
	}

	public async Task<bool> ExistsActiveForEmployeeAndEffectiveDateAsync(
		string employeeId,
		DateOnly effectiveFrom,
		string? excludeId = null,
		CancellationToken cancellationToken = default)
	{
		return await _context.HrmEmployeeTransfers
			.AnyAsync(x => x.IsActive
				&& x.EmployeeId == employeeId
				&& x.EffectiveFrom == effectiveFrom
				&& (excludeId == null || x.Id != excludeId), cancellationToken);
	}

	private IQueryable<HrmEmployeeTransfer> BuildQuery(bool includeInactive)
	{
		var query = _context.HrmEmployeeTransfers
			.Include(x => x.Employee)
			.Include(x => x.PreviousDepartment)
			.Include(x => x.PreviousSection)
			.Include(x => x.NewDepartment)
			.Include(x => x.NewSection)
			.AsQueryable();

		if (!includeInactive)
			query = query.Where(x => x.IsActive);

		return query;
	}
}
