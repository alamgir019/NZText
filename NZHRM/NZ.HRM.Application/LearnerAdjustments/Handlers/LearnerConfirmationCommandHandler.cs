using NZ.HRM.Application.Common;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.LearnerAdjustments.Commands;
using NZ.HRM.Application.Model.LearnerAdjustments.DTOs;
using NZ.HRM.Domain.Services;

namespace NZ.HRM.Application.LearnerAdjustments.Handlers;

public class LearnerConfirmationCommandHandler
{
    private readonly ILearnerConfirmationRepository _repository;

    public LearnerConfirmationCommandHandler(ILearnerConfirmationRepository repository)
    {
        _repository = repository;
    }

    public async Task<LearnerConfirmationBatchResultDto> Handle(
        ForwardLearnersForConfirmationCommand command,
        CancellationToken cancellationToken = default)
    {
        if (command.EmployeeIds is null || command.EmployeeIds.Count == 0)
            throw new BusinessRuleException("EMPLOYEE_LIST_REQUIRED",
                "At least one employee must be selected.");

        if (!ProbationAdjustmentPolicy.IsSupportedProbationPeriod(command.ProbationPeriodMonths))
            throw new BusinessRuleException("INVALID_PROBATION_PERIOD",
                "Selected probation period is not valid.");

        if (string.IsNullOrWhiteSpace(command.ForwardedBy))
            throw new BusinessRuleException("FORWARDED_BY_REQUIRED",
                "Forwarded By is required.");

        command.EmployeeIds = Normalize(command.EmployeeIds);

        return await _repository.ForwardAsync(command, cancellationToken);
    }

    public async Task<LearnerConfirmationBatchResultDto> Handle(
        ApproveLearnerConfirmationsCommand command,
        CancellationToken cancellationToken = default)
    {
        if (command.EmployeeIds is null || command.EmployeeIds.Count == 0)
            throw new BusinessRuleException("EMPLOYEE_LIST_REQUIRED",
                "At least one employee must be selected.");

        if (string.IsNullOrWhiteSpace(command.ApprovedBy))
            throw new BusinessRuleException("APPROVED_BY_REQUIRED",
                "Approved By is required.");

        command.EmployeeIds = Normalize(command.EmployeeIds);

        return await _repository.ApproveAsync(command, cancellationToken);
    }

    public Task<List<PendingLearnerConfirmationDto>> HandlePending(
        CancellationToken cancellationToken = default)
        => _repository.GetPendingAsync(cancellationToken);

    private static List<string> Normalize(List<string> employeeIds)
        => employeeIds
            .Where(id => !string.IsNullOrWhiteSpace(id))
            .Select(id => id.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();
}
