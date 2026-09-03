namespace NZ.HRM.Application.LearnerAdjustments.Commands;

/// <summary>
/// Forwards a list of learner employees for permanency (confirmation) approval.
/// </summary>
public class ForwardLearnersForConfirmationCommand
{
    public List<string> EmployeeIds { get; set; } = new();

    /// <summary>
    /// Probation duration in months used to compute the probation completion date.
    /// </summary>
    public int ProbationPeriodMonths { get; set; }

    public string ForwardedBy { get; set; } = string.Empty;

    public string? Remarks { get; set; }
}
