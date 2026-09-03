namespace NZ.HRM.Application.LearnerAdjustments.Commands;

/// <summary>
/// Approves (or rejects) a list of forwarded learner permanency requests.
/// </summary>
public class ApproveLearnerConfirmationsCommand
{
    /// <summary>
    /// Employee IDs whose pending requests should be actioned.
    /// </summary>
    public List<string> EmployeeIds { get; set; } = new();

    public bool Approved { get; set; } = true;

    public string ApprovedBy { get; set; } = string.Empty;

    public string? Remarks { get; set; }
}
