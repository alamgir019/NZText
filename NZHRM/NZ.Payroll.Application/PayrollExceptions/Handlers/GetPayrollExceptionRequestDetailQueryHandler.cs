using NZ.Payroll.Application.Interfaces.Repositories;
using NZ.Payroll.Application.PayrollExceptions.DTOs;
using NZ.Payroll.Application.PayrollExceptions.Queries;

namespace NZ.Payroll.Application.PayrollExceptions.Handlers;

public class GetPayrollExceptionRequestDetailQueryHandler
{
    private readonly IPayrollExceptionRepository _repository;

    public GetPayrollExceptionRequestDetailQueryHandler(IPayrollExceptionRepository repository)
    {
        _repository = repository;
    }

    public Task<PayrollExceptionRequestDetailDto?> Handle(GetPayrollExceptionRequestDetailQuery query, CancellationToken cancellationToken = default)
        => _repository.GetDetailByIdAsync(query.RequestId, cancellationToken);
}
