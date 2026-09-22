using Microsoft.EntityFrameworkCore;
using NZ.HRM.Domain.Constants;
using NZ.HRM.Domain.Entities;
using NZ.Payroll.Application.Interfaces.Repositories;
using NZ.Payroll.Infrastructure.Persistence;

namespace NZ.Payroll.Infrastructure.Repositories;

public class PayIncrementHistoryRepository : IPayIncrementHistoryRepository
{
	private readonly PayrollDbContext _context;

	public PayIncrementHistoryRepository(PayrollDbContext context)
	{
		_context = context;
	}

	public async Task<PayIncrementHistory> AddAsync(PayIncrementHistory entity, CancellationToken cancellationToken = default)
	{
		await _context.PayIncrementHistories.AddAsync(entity, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
		return entity;
	}

	public Task<bool> ExistsByEmployeeAndEffectiveDateAsync(string employeeId, DateOnly effectiveDate, CancellationToken cancellationToken = default)
	{
		return _context.PayIncrementHistories.AnyAsync(
			history => history.EmployeeId == employeeId && history.EffectiveDate == effectiveDate,
			cancellationToken);
	}

	public async Task<IEnumerable<PayIncrementHistory>> GetByIdsAsync(
		IEnumerable<string> ids,
		CancellationToken cancellationToken = default)
	{
		var historyIds = ids.ToList();

		return await _context.PayIncrementHistories
			.Where(history => historyIds.Contains(history.Id) && history.IsActive)
			.ToListAsync(cancellationToken);
	}
	public async Task<IEnumerable<PayIncrementHistory>> AddRangeAsync(IEnumerable<PayIncrementHistory> entities, CancellationToken cancellationToken = default)
	{
		await _context.PayIncrementHistories.AddRangeAsync(entities, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
		return entities;
	}

	public async Task<IEnumerable<PayIncrementHistory>> AddHistoriesWithRequestsAsync(
		IEnumerable<PayIncrementHistory> histories,
		IEnumerable<PerIncrementRequest> requests,
		CancellationToken cancellationToken = default)
	{
		await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

		try
		{
			await _context.PayIncrementHistories.AddRangeAsync(histories, cancellationToken);
			await _context.PerIncrementRequests.AddRangeAsync(requests, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);
			await transaction.CommitAsync(cancellationToken);

			return histories;
		}
		catch
		{
			await transaction.RollbackAsync(cancellationToken);
			throw;
		}
	}

	public async Task<IEnumerable<PerIncrementRequest>> UpdateHistoriesWithRequestsAndSalaryAsync(
		IEnumerable<PayIncrementHistory> histories,
		IEnumerable<PerIncrementRequest> requests,
		CancellationToken cancellationToken = default)
	{
		var historyList = histories.ToList();
		var requestList = requests.ToList();
		await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

		try
		{
			_context.PayIncrementHistories.UpdateRange(historyList);
			await _context.PerIncrementRequests.AddRangeAsync(requestList, cancellationToken);

			foreach (var history in historyList.Where(history =>
				string.Equals(history.Status, PayIncrementStatuses.Approved, StringComparison.OrdinalIgnoreCase)))
			{
				if (!history.NewGrossSalary.HasValue || history.NewGrossSalary.Value <= 2000m)
					throw new ArgumentException("New gross salary must be greater than 2000 when approving a pay increment");

				var grossSalary = history.NewGrossSalary.Value;
				var basicSalary = (grossSalary - 2000m) / 1.55m;
				var houseRent = basicSalary * 0.55m;

				var salaryStructure = await _context.PaySalaryStructures
					.FirstOrDefaultAsync(structure =>
						structure.EmployeeId == history.EmployeeId &&
						structure.ActiveFlag,
						cancellationToken);

				if (salaryStructure == null)
				{
					salaryStructure = new PaySalaryStructure
					{
						EmployeeId = history.EmployeeId,
						EffectiveDate = history.EffectiveDate,
						ActiveFlag = true,
						CreatedDate = DateTime.UtcNow
					};
					await _context.PaySalaryStructures.AddAsync(salaryStructure, cancellationToken);
				}

				salaryStructure.GrossSalary = grossSalary;
				salaryStructure.BasicSalary = basicSalary;
				salaryStructure.HouseRent = houseRent;
				salaryStructure.MedicalAllowance = 750m;
				salaryStructure.FoodAllowance = 850m;
				salaryStructure.ConveyanceAllowance = 400m;

				var employeePayroll = await _context.HrmEmployeePayrolls
					.FirstOrDefaultAsync(payroll =>
						payroll.EmployeeId == history.EmployeeId &&
						payroll.IsActive,
						cancellationToken);

				if (employeePayroll == null)
				{
					employeePayroll = new HrmEmployeePayroll
					{
						EmployeeId = history.EmployeeId,
						IsActive = true
					};
					await _context.HrmEmployeePayrolls.AddAsync(employeePayroll, cancellationToken);
				}

				employeePayroll.GrossSalary = grossSalary;
				employeePayroll.BasicSalary = basicSalary;
				employeePayroll.HouseRentAllowance = houseRent;
				employeePayroll.MedicalAllowance = 750m;
				employeePayroll.FoodAllowance = 850m;
				employeePayroll.ConveyanceAllowance = 400m;
			}

			await _context.SaveChangesAsync(cancellationToken);
			await transaction.CommitAsync(cancellationToken);

			return requestList;
		}
		catch
		{
			await transaction.RollbackAsync(cancellationToken);
			throw;
		}
	}

	public Task<PayIncrementHistory?> GetByIdAsync(
		string id,
		CancellationToken cancellationToken = default)
	{
		return _context.PayIncrementHistories
			.FirstOrDefaultAsync(
				history => history.Id == id && history.IsActive,
				cancellationToken);
	}

	public async Task<PerIncrementRequest> UpdateHistoryWithRequestAsync(
		PayIncrementHistory history,
		PerIncrementRequest request,
		CancellationToken cancellationToken = default)
	{
		await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

		try
		{
			_context.PayIncrementHistories.Update(history);
			await _context.PerIncrementRequests.AddAsync(request, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);
			await transaction.CommitAsync(cancellationToken);

			return request;
		}
		catch
		{
			await transaction.RollbackAsync(cancellationToken);
			throw;
		}
	}

	public async Task<IEnumerable<PerIncrementRequest>> UpdateHistoriesWithRequestsAsync(
		IEnumerable<PayIncrementHistory> histories,
		IEnumerable<PerIncrementRequest> requests,
		CancellationToken cancellationToken = default)
	{
		await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

		try
		{
			_context.PayIncrementHistories.UpdateRange(histories);
			await _context.PerIncrementRequests.AddRangeAsync(requests, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);
			await transaction.CommitAsync(cancellationToken);

			return requests;
		}
		catch
		{
			await transaction.RollbackAsync(cancellationToken);
			throw;
		}
	}

	public async Task<IEnumerable<PayIncrementHistory>> GetByStatusWithRequestsAsync(
		string status,
		CancellationToken cancellationToken = default)
	{
		return await _context.PayIncrementHistories
			.Include(history => history.Employee)
			.Include(history => history.PerIncrementRequests.Where(request => request.IsActive))
			.Where(history =>
				history.IsActive &&
				history.Status == status)
			.ToListAsync(cancellationToken);
	}

	public async Task<IEnumerable<PayIncrementHistory>> GetPreviousHistoriesAsync(
		IEnumerable<string> employeeIds,
		IEnumerable<string> currentHistoryIds,
		CancellationToken cancellationToken = default)
	{
		var employeeIdList = employeeIds.ToList();
		var currentHistoryIdList = currentHistoryIds.ToList();

		return await _context.PayIncrementHistories
			.Where(history =>
				history.IsActive &&
				employeeIdList.Contains(history.EmployeeId) &&
				!currentHistoryIdList.Contains(history.Id))
			.OrderByDescending(history => history.EffectiveDate)
			.ToListAsync(cancellationToken);
	}
}
