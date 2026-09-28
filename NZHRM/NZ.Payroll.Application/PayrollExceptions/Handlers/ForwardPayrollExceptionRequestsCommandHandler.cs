using NZ.Payroll.Application.Interfaces.Repositories;
using NZ.Payroll.Application.PayrollExceptions.Commands;
using NZ.Payroll.Domain.Enums;
using NZ.Payroll.Domain.Services;

namespace NZ.Payroll.Application.PayrollExceptions.Handlers;

public class ForwardPayrollExceptionRequestsCommandHandler
{
    private readonly IPayrollExceptionRepository _repository;
    private readonly PayrollExceptionWorkflow _workflow;

    public ForwardPayrollExceptionRequestsCommandHandler(IPayrollExceptionRepository repository, PayrollExceptionWorkflow workflow)
    {
        _repository = repository;
        _workflow = workflow;
    }

    public async Task<ForwardPayrollExceptionRequestsResult> Handle(ForwardPayrollExceptionRequestsCommand command, CancellationToken cancellationToken = default)
    {
        if (command == null)
        {
            return Error("INVALID_REQUEST", "Request payload is required.");
        }

        var requestIds = (command.RequestIds ?? new List<string>())
            .Where(id => !string.IsNullOrWhiteSpace(id))
            .Select(id => id.Trim())
            .ToList();

        if (requestIds.Count == 0)
        {
            return Error("NO_REQUEST_SELECTED", "At least one payroll exception request must be selected.");
        }

        if (requestIds.Count != requestIds.Distinct(StringComparer.OrdinalIgnoreCase).Count())
        {
            return Error("INVALID_REQUEST", "Duplicate request IDs are not allowed.");
        }

        var remarks = string.IsNullOrWhiteSpace(command.Remarks)
            ? null
            : command.Remarks.Trim();

        if (remarks?.Length > 250)
        {
            return Error("INVALID_REQUEST", "Remarks cannot exceed 250 characters.");
        }

        var forwardedBy = string.IsNullOrWhiteSpace(command.ForwardedBy)
            ? "SYSTEM"
            : command.ForwardedBy.Trim();

        var requests = await _repository.GetByIdsAsync(requestIds, cancellationToken);
        if (requests.Count == 0)
        {
            return Error("REQUEST_NOT_FOUND", "Payroll exception request not found.");
        }

        if (requests.Count != requestIds.Count)
        {
            return Error("INVALID_REQUEST", "One or more request IDs are invalid.");
        }

        foreach (var request in requests)
        {
            var status = GetStatus(request);

            if (string.Equals(status, PayrollExceptionStatuses.ForwardedToIT, StringComparison.OrdinalIgnoreCase))
            {
                return Error("REQUEST_ALREADY_FORWARDED", "Selected request has already been forwarded to Head Office IT.");
            }

            if (string.Equals(status, PayrollExceptionStatuses.Rejected, StringComparison.OrdinalIgnoreCase))
            {
                return Error("INVALID_REQUEST", "Rejected requests cannot be forwarded.");
            }

            if (!string.Equals(status, PayrollExceptionStatuses.Pending, StringComparison.OrdinalIgnoreCase))
            {
                return Error("INVALID_REQUEST", "Only pending payroll exception requests can be forwarded.");
            }
        }

        foreach (var request in requests)
        {
            ForwardToIT(request, forwardedBy);
        }

        await _repository.SaveForwardingAsync(requests, forwardedBy, remarks, cancellationToken);

        return new ForwardPayrollExceptionRequestsResult
        {
            Success = true,
            Message = "Selected requests have been forwarded to Head Office IT.",
            ForwardedCount = requests.Count,
            ForwardedOn = DateTime.UtcNow
        };
    }

    private static string? GetStatus(object request) =>
        request.GetType().GetProperty("Status")?.GetValue(request)?.ToString();

    private void ForwardToIT(object request, string forwardedBy)
    {
        var method = _workflow.GetType().GetMethods()
            .FirstOrDefault(m =>
            {
                if (!string.Equals(m.Name, "ForwardToIT", StringComparison.Ordinal))
                {
                    return false;
                }

                var parameters = m.GetParameters();
                return parameters.Length == 2
                       && parameters[1].ParameterType == typeof(string)
                       && parameters[0].ParameterType.IsInstanceOfType(request);
            });

        if (method == null)
        {
            throw new InvalidOperationException("ForwardToIT workflow method was not found for the repository entity type.");
        }

        method.Invoke(_workflow, new[] { request, forwardedBy });
    }

    private static ForwardPayrollExceptionRequestsResult Error(string errorCode, string message) =>
        new()
        {
            Success = false,
            ErrorCode = errorCode,
            Message = message
        };
}
