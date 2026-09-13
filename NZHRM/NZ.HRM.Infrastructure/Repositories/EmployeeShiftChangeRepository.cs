using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class EmployeeShiftChangeRepository : IEmployeeShiftChangeRepository
{
	private readonly ApplicationDbContext _context;

	public EmployeeShiftChangeRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<List<HrmEmployeeShiftChange>> GetAllAsync(
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

	public async Task<HrmEmployeeShiftChange?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
	{
		return await BuildQuery(false)
			.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
	}

	public async Task<List<HrmEmployeeShiftChange>> GetByEmployeeIdAsync(
		string employeeId,
		bool includeInactive = false,
		DateOnly? fromDate = null,
		DateOnly? toDate = null,
		CancellationToken cancellationToken = default)
	{
		return await GetAllAsync(includeInactive, employeeId, fromDate, toDate, cancellationToken);
	}

	public async Task<HrmEmployeeShiftChange?> GetLatestByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken = default)
	{
		return await BuildQuery(false)
			.Where(x => x.EmployeeId == employeeId)
			.OrderByDescending(x => x.EffectiveFrom)
			.ThenByDescending(x => x.CreatedOn)
			.FirstOrDefaultAsync(cancellationToken);
	}

	public async Task<string> AddAsync(HrmEmployeeShiftChange shiftChange, CancellationToken cancellationToken = default)
	{
		_context.HrmEmployeeShiftChanges.Add(shiftChange);
		await _context.SaveChangesAsync(cancellationToken);
		return shiftChange.Id;
	}

	public async Task<string> AddWithEmploymentAsync(
		HrmEmployeeShiftChange shiftChange,
		HrmEmployeeEmployment employment,
		CancellationToken cancellationToken = default)
	{
		await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

		_context.HrmEmployeeShiftChanges.Add(shiftChange);
		_context.HrmEmployeeEmployments.Update(employment);
		await _context.SaveChangesAsync(cancellationToken);
		await transaction.CommitAsync(cancellationToken);

		return shiftChange.Id;
	}

	public async Task UpdateAsync(HrmEmployeeShiftChange shiftChange, CancellationToken cancellationToken = default)
	{
		_context.HrmEmployeeShiftChanges.Update(shiftChange);
		await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task UpdateWithEmploymentAsync(
		HrmEmployeeShiftChange shiftChange,
		HrmEmployeeEmployment employment,
		CancellationToken cancellationToken = default)
	{
		await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

		_context.HrmEmployeeShiftChanges.Update(shiftChange);
		_context.HrmEmployeeEmployments.Update(employment);
		await _context.SaveChangesAsync(cancellationToken);
		await transaction.CommitAsync(cancellationToken);
	}

	public async Task DeleteAsync(HrmEmployeeShiftChange shiftChange, CancellationToken cancellationToken = default)
	{
		_context.HrmEmployeeShiftChanges.Remove(shiftChange);
		await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task DeleteWithEmploymentAsync(
		HrmEmployeeShiftChange shiftChange,
		HrmEmployeeEmployment employment,
		CancellationToken cancellationToken = default)
	{
		await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

		_context.HrmEmployeeShiftChanges.Update(shiftChange);
		_context.HrmEmployeeEmployments.Update(employment);
		await _context.SaveChangesAsync(cancellationToken);
		await transaction.CommitAsync(cancellationToken);
	}

	public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
	{
		return await _context.HrmEmployeeShiftChanges
			.AnyAsync(x => x.Id == id, cancellationToken);
	}

	public async Task<bool> ExistsActiveForEmployeeAndEffectiveDateAsync(
		string employeeId,
		DateOnly effectiveFrom,
		string? excludeId = null,
		CancellationToken cancellationToken = default)
	{
		return await _context.HrmEmployeeShiftChanges
			.AnyAsync(x => x.IsActive
				&& x.EmployeeId == employeeId
				&& x.EffectiveFrom == effectiveFrom
				&& (excludeId == null || x.Id != excludeId), cancellationToken);
	}

	private IQueryable<HrmEmployeeShiftChange> BuildQuery(bool includeInactive)
	{
		var query = _context.HrmEmployeeShiftChanges
			.Include(x => x.Employee)
			.Include(x => x.PreviousShift)
			.Include(x => x.NewShift)
			.AsQueryable();

		if (!includeInactive)
			query = query.Where(x => x.IsActive);

		return query;
	}
}
