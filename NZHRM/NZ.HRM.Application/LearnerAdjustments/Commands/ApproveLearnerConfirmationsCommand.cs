namespace NZ.HRM.Application.LearnerAdjustments.Commands;

/// <summary>
/// Approves or rejects a list of forwarded learner permanency requests.
/// </summary>
public class ApproveLearnerConfirmationsCommand
{
    /// <summary>
    /// Requests whose pending items should be actioned.
    /// </summary>
    public List<LearnerConfirmationActionDto> Requests { get; set; } = new();

    public string ApprovedBy { get; set; } = string.Empty;
}

/// <summary>
/// A single learner confirmation action in a batch request.
/// </summary>
public class LearnerConfirmationActionDto
{
    public string RequestId { get; set; } = string.Empty;
    public bool Approved { get; set; } = true;
    public string? Remarks { get; set; }
}


/// <summary>
/// A single learner confirmation action in a batch request.
/// </summary>
public class LearnerConfirmationsCommand
{
    public string RequestId { get; set; } = string.Empty;
    public string ForwardedBy { get; set; } = string.Empty;
    public string? Remarks { get; set; }
}
