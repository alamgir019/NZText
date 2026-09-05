namespace NZ.HRM.Application.Model.LearnerAdjustments.DTOs;

/// <summary>
/// Result of processing a single employee within a forward/approve batch.
/// </summary>
public class LearnerConfirmationResultItemDto
{
    public string EmployeeId { get; set; } = string.Empty;
    public string? RequestId { get; set; }
    public string Status { get; set; } = string.Empty;
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
}

public class LearnerConfirmationBatchResultDto
{
    public int TotalRequested { get; set; }
    public int SucceededCount { get; set; }
    public int FailedCount { get; set; }
    public List<LearnerConfirmationResultItemDto> Items { get; set; } = new();
}

/// <summary>
/// A learner permanency request pending approval.
/// </summary>
public class PendingLearnerConfirmationDto
{
    public string RequestId { get; set; } = string.Empty;
    public string EmployeeId { get; set; } = string.Empty;
    public string EmployeeCode { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
    public string DepartmentName { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
    public DateOnly DateOfJoining { get; set; }
    public DateOnly ProbationCompletedOn { get; set; }
    public decimal CurrentGrossSalary { get; set; }
    public decimal StandardGrossSalary { get; set; }
    public decimal AdjustmentAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public string ForwardedBy { get; set; } = string.Empty;
    public DateTime ForwardedOn { get; set; }
}
