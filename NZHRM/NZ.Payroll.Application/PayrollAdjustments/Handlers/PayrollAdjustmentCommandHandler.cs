using System.Text.Json;
using NZ.HRM.Domain.Entities;
using NZ.Payroll.Application.Interfaces.Repositories;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.Payroll.Application.PayrollAdjustments.Commands;

namespace NZ.Payroll.Application.PayrollAdjustments.Handlers;

public class PayrollAdjustmentCommandHandler
{
    private readonly IPayrollAdjustmentRepository _repository;
    private readonly IEmployeeMasterRepository _employeeQuery;
    private readonly IPayrollAdjustmentHistoryRepository _historyRepository;

    public PayrollAdjustmentCommandHandler(IPayrollAdjustmentRepository repository, IEmployeeMasterRepository employeeQuery, IPayrollAdjustmentHistoryRepository historyRepository)
    {
        _repository = repository;
        _employeeQuery = employeeQuery;
        _historyRepository = historyRepository;
    }

    public async Task<string> Handle(CreatePayrollAdjustmentCommand command, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(command.EmployeeId)) throw new ArgumentException("EmployeeId is required");

        var exists = await _employeeQuery.ExistsAsync(command.EmployeeId, cancellationToken);
        if (!exists) throw new KeyNotFoundException($"Employee '{command.EmployeeId}' not found");

        var entity = new PayPayrollAdjustment
        {
            EmployeeId = command.EmployeeId,
            PayrollMonth = command.AttendanceMonth,
            AdjustmentType = command.CorrectionType,
            Reason = command.Reason,
            OldAmount = null,
            NewAmount = null,
            AdjustmentDate = DateTime.UtcNow,
            IsActive = true
        };

        var saved = await _repository.AddAsync(entity, cancellationToken);

        // record history
        try
        {
            var history = new PayPayrollAdjustmentHistory
            {
                PayrollAdjustmentId = saved.Id,
                Action = "CREATED",
                PerformedBy = "SYSTEM",
                PerformedOn = DateTime.UtcNow,
                Notes = command.Remarks,
                NewData = JsonSerializer.Serialize(saved)
            };
            await _historyRepository.AddAsync(history, cancellationToken);
        }
        catch { /* do not fail main flow on history write */ }

        return saved.Id;
    }

    public async Task Handle(UpdatePayrollAdjustmentCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity == null) throw new KeyNotFoundException($"Payroll adjustment '{command.Id}' not found");

        // only allow update in certain states - simplified: allow if active
        if (!entity.IsActive) throw new InvalidOperationException("Cannot update inactive/cancelled adjustment");

        var oldData = JsonSerializer.Serialize(entity);

        entity.AdjustmentType = command.CorrectionType;
        entity.Reason = command.Reason;
        entity.PayrollMonth = command.AttendanceMonth;
        entity.UpdatedOn = DateTime.UtcNow;

        await _repository.UpdateAsync(entity, cancellationToken);

        // record history
        try
        {
            var history = new PayPayrollAdjustmentHistory
            {
                PayrollAdjustmentId = entity.Id,
                Action = "UPDATED",
                PerformedBy = "SYSTEM",
                PerformedOn = DateTime.UtcNow,
                Notes = command.Remarks,
                OldData = oldData,
                NewData = JsonSerializer.Serialize(entity)
            };
            await _historyRepository.AddAsync(history, cancellationToken);
        }
        catch { }
    }

    public async Task HandleDelete(string id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity == null) throw new KeyNotFoundException($"Payroll adjustment '{id}' not found");
        if (!entity.IsActive) throw new InvalidOperationException("Adjustment already inactive");

        // Soft delete by marking inactive
        var oldData = JsonSerializer.Serialize(entity);
        entity.IsActive = false;
        entity.UpdatedOn = DateTime.UtcNow;
        await _repository.UpdateAsync(entity, cancellationToken);

        try
        {
            var history = new PayPayrollAdjustmentHistory
            {
                PayrollAdjustmentId = entity.Id,
                Action = "CANCELLED",
                PerformedBy = "SYSTEM",
                PerformedOn = DateTime.UtcNow,
                Notes = null,
                OldData = oldData,
                NewData = JsonSerializer.Serialize(entity)
            };
            await _historyRepository.AddAsync(history, cancellationToken);
        }
        catch { }
    }
}
