namespace NZ.HRM.Application.LearnerAdjustments.Queries;

/// <summary>
/// Normalized and validated filter passed from the Application layer to the repository.
/// </summary>
public sealed record EligibleLearnerFilter(
    DateOnly JoiningDateFrom,
    DateOnly JoiningDateTo,
    int ProbationPeriodMonths,
    DateOnly BusinessDate,
    int PageNumber,
    int PageSize,
    bool NoPaging);
