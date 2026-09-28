using NZ.Payroll.Application.Interfaces.Repositories;
using NZ.Payroll.Application.PayrollExceptions.DTOs;
using NZ.Payroll.Application.PayrollExceptions.Queries;

namespace NZ.Payroll.Application.PayrollExceptions.Handlers;

public class GetPayrollExceptionRequestsQueryHandler
{
    private readonly IPayrollExceptionRepository _repository;

    public GetPayrollExceptionRequestsQueryHandler(IPayrollExceptionRepository repository)
    {
        _repository = repository;
    }
    // if payroll exception is used then move this handler to the payroll adjustment folder
    public Task<PayrollExceptionRequestsResponseDto> Handle(GetPayrollExceptionRequestsQuery query, CancellationToken cancellationToken = default)
        => _repository.GetPayrollExceptionRequestsAsync(query, cancellationToken);
}
