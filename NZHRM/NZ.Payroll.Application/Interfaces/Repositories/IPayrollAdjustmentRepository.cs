using NZ.HRM.Domain.Entities;

namespace NZ.Payroll.Application.Interfaces.Repositories;

public interface IPayrollAdjustmentRepository
{
    Task<PayPayrollAdjustment> AddAsync(PayPayrollAdjustment entity, CancellationToken cancellationToken = default);
    Task<PayPayrollAdjustment?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<(List<PayPayrollAdjustment> items, int total)> GetAllAsync(
        string? attendanceMonth = null,
        string? companyId = null,
        string? employeeId = null,
        string? status = null,
        int page = 1,
        int pageSize = 20,
        CancellationToken cancellationToken = default);
    Task UpdateAsync(PayPayrollAdjustment entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(PayPayrollAdjustment entity, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
}
