using NZ.Payroll.Application.PayrollAdjustments.DTOs;
using NZ.Payroll.Application.PayrollAdjustments.Queries;
using NZ.Payroll.Application.Interfaces.Repositories;

namespace NZ.Payroll.Application.PayrollAdjustments.Handlers;

public class GetAllPayrollAdjustmentsQueryHandler
{
    private readonly IPayrollAdjustmentRepository _repository;

    public GetAllPayrollAdjustmentsQueryHandler(IPayrollAdjustmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<(List<PayrollAdjustmentDto> items, int total)> Handle(GetAllPayrollAdjustmentsQuery query, CancellationToken cancellationToken = default)
    {
        var (entities, total) = await _repository.GetAllAsync(query.AttendanceMonth, query.CompanyId, query.EmployeeId, query.Status, query.Page, query.PageSize, cancellationToken);
        var items = entities.Select(e => new PayrollAdjustmentDto
        {
            Id = e.Id,
            EmployeeId = e.EmployeeId,
            AttendanceMonth = e.PayrollMonth,
            AdjustmentType = e.AdjustmentType,
            OldAmount = e.OldAmount,
            NewAmount = e.NewAmount,
            Reason = e.Reason,
            SubmittedOn = e.CreatedOn
        }).ToList();

        return (items, total);
    }
}
