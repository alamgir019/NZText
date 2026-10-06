using NZ.HRM.Application.LearnerAdjustments.Commands;
using NZ.HRM.Application.Model.LearnerAdjustments.DTOs;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface ILearnerConfirmationRepository
{
    /// <summary>
    /// Creates pending permanency requests for the supplied learners.
    /// </summary>
    Task<LearnerConfirmationBatchResultDto> ForwardAsync(
        ForwardLearnersForConfirmationCommand command,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Approves or rejects the pending permanency requests of the supplied learners.
    /// On approval the confirmation date and standard gross salary are applied to the employee.
    /// </summary>
    Task<LearnerConfirmationBatchResultDto> ApproveAsync(
        ApproveLearnerConfirmationsCommand command,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the permanency requests currently awaiting approval.
    /// </summary>
    Task<List<PendingLearnerConfirmationDto>> GetPendingAsync(
string? status, CancellationToken cancellationToken = default);
    Task<LearnerConfirmationBatchResultDto> ForwardToMovementCellAsync(List<LearnerConfirmationsCommand> command, CancellationToken cancellationToken);
    Task<LearnerConfirmationBatchResultDto> ForwardToMovementSectionAsync(List<LearnerConfirmationsCommand> commands, CancellationToken cancellationToken);
    Task<LearnerConfirmationBatchResultDto> ForwardToMovementHRAsync(List<LearnerConfirmationsCommand> commands, CancellationToken cancellationToken);
}
