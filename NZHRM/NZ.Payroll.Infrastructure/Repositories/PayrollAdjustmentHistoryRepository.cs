using Microsoft.EntityFrameworkCore;
using NZ.HRM.Domain.Entities;
using NZ.Payroll.Application.Interfaces.Repositories;
using NZ.Payroll.Infrastructure.Persistence;

namespace NZ.Payroll.Infrastructure.Repositories;

public class PayrollAdjustmentHistoryRepository : IPayrollAdjustmentHistoryRepository
{
    private readonly PayrollDbContext _context;

    public PayrollAdjustmentHistoryRepository(PayrollDbContext context)
    {
        _context = context;
    }

    public async Task<PayPayrollAdjustmentHistory> AddAsync(PayPayrollAdjustmentHistory entity, CancellationToken cancellationToken = default)
    {
        await _context.PayPayrollAdjustmentHistories.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }
}
