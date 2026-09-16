using NZ.Payroll.Application.PayrollAdjustments.DTOs;
using NZ.Payroll.Application.Interfaces.Repositories;

namespace NZ.Payroll.Application.PayrollAdjustments.Handlers;

public class GetPayrollAdjustmentByIdQueryHandler
{
    private readonly IPayrollAdjustmentRepository _repository;

    public GetPayrollAdjustmentByIdQueryHandler(IPayrollAdjustmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<PayrollAdjustmentDto?> Handle(string id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity == null) return null;

        return new PayrollAdjustmentDto
        {
            Id = entity.Id,
            EmployeeId = entity.EmployeeId,
            AttendanceMonth = entity.PayrollMonth,
            AdjustmentType = entity.AdjustmentType,
            OldAmount = entity.OldAmount,
            NewAmount = entity.NewAmount,
            Reason = entity.Reason,
            SubmittedOn = entity.CreatedOn
        };
    }
}
