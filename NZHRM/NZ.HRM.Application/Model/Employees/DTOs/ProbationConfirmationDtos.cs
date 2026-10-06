namespace NZ.HRM.Application.Model.Employees.DTOs;

public class ProbationCompletionListItemDto
{
    public string EmployeeId { get; set; } = string.Empty;
    public string EmployeeCode { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string Section { get; set; } = string.Empty;
    public DateOnly DateOfJoining { get; set; }
    public DateOnly ProbationEndDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public bool ConfirmationEligible { get; set; }
}

public class ProbationCompletionPagedResultDto
{
    public int Total { get; set; }
    public List<ProbationCompletionListItemDto> Items { get; set; } = new();
}

public class ProbationConfirmationResultItemDto
{
    public string EmployeeId { get; set; } = string.Empty;
    public string? LetterDocumentId { get; set; }
    public string? DocumentPath { get; set; }
    public DateOnly ConfirmationDate { get; set; }
    public DateTime GeneratedAt { get; set; }
    public string ConfirmedBy { get; set; } = string.Empty;
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
}

public class ProbationConfirmationBatchResultDto
{
    public int TotalRequested { get; set; }
    public int SucceededCount { get; set; }
    public int FailedCount { get; set; }
    public List<ProbationConfirmationResultItemDto> Items { get; set; } = new();
}
