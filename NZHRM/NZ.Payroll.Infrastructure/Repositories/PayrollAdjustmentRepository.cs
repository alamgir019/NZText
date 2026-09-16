using Microsoft.EntityFrameworkCore;
using NZ.HRM.Domain.Entities;
using NZ.Payroll.Application.Interfaces.Repositories;
using NZ.Payroll.Infrastructure.Persistence;

namespace NZ.Payroll.Infrastructure.Repositories;

public class PayrollAdjustmentRepository : IPayrollAdjustmentRepository
{
    private readonly PayrollDbContext _context;

    public PayrollAdjustmentRepository(PayrollDbContext context)
    {
        _context = context;
    }

    public async Task<PayPayrollAdjustment> AddAsync(PayPayrollAdjustment entity, CancellationToken cancellationToken = default)
    {
        await _context.PayPayrollAdjustments.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public Task<PayPayrollAdjustment?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return _context.PayPayrollAdjustments.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<(List<PayPayrollAdjustment> items, int total)> GetAllAsync(
        string? attendanceMonth = null,
        string? companyId = null,
        string? employeeId = null,
        string? status = null,
        int page = 1,
        int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var query = _context.PayPayrollAdjustments.AsQueryable();
        if (!string.IsNullOrWhiteSpace(attendanceMonth)) query = query.Where(x => x.PayrollMonth == attendanceMonth);
        if (!string.IsNullOrWhiteSpace(employeeId)) query = query.Where(x => x.EmployeeId == employeeId);
        // status/company filter not available on entity; ignoring companyId/status for now

        query = query.OrderByDescending(x => x.CreatedOn);
        var total = await query.CountAsync(cancellationToken);
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return (items, total);
    }

    public async Task UpdateAsync(PayPayrollAdjustment entity, CancellationToken cancellationToken = default)
    {
        _context.PayPayrollAdjustments.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PayPayrollAdjustment entity, CancellationToken cancellationToken = default)
    {
        _context.PayPayrollAdjustments.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return _context.PayPayrollAdjustments.AnyAsync(x => x.Id == id, cancellationToken);
    }
}
