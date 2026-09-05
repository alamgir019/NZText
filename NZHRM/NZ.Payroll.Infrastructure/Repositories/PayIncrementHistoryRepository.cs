using Microsoft.EntityFrameworkCore;
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
}
