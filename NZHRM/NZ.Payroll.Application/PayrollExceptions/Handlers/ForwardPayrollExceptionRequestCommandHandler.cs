using NZ.Payroll.Application.PayrollExceptions.Commands;

namespace NZ.Payroll.Application.PayrollExceptions.Handlers;

public class ForwardPayrollExceptionRequestCommandHandler
{
    private readonly ForwardPayrollExceptionRequestsCommandHandler _batchHandler;

    public ForwardPayrollExceptionRequestCommandHandler(ForwardPayrollExceptionRequestsCommandHandler batchHandler)
    {
        _batchHandler = batchHandler;
    }

    public Task<ForwardPayrollExceptionRequestsResult> Handle(ForwardPayrollExceptionRequestCommand command, CancellationToken cancellationToken = default)
    {
        return _batchHandler.Handle(new ForwardPayrollExceptionRequestsCommand
        {
            RequestIds = new List<string> { command.RequestId },
            Remarks = command.Remarks,
            ForwardedBy = command.ForwardedBy
        }, cancellationToken);
    }
}
