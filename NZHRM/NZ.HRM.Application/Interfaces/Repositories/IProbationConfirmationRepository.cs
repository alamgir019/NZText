using NZ.HRM.Application.Employees.Commands.ConfirmProbationEmployees;
using NZ.HRM.Application.Employees.Queries.GetProbationCompletion;
using NZ.HRM.Application.Model.Employees.DTOs;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IProbationConfirmationRepository
{
    Task<ProbationCompletionPagedResultDto> GetEligibleEmployeesAsync(
        GetProbationCompletionQuery query,
        CancellationToken cancellationToken = default);

    Task<ProbationConfirmationBatchResultDto> ConfirmEmployeesAsync(
        ConfirmProbationEmployeesCommand command,
        string confirmedBy,
        CancellationToken cancellationToken = default);
}
