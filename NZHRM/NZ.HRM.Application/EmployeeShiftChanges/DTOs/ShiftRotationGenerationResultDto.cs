namespace NZ.HRM.Application.EmployeeShiftChanges.DTOs;

public class ShiftRotationGenerationResultDto
{
    public int TotalEmployees { get; set; }
    public int ProcessedEmployees { get; set; }
    public int SkippedEmployees { get; set; }
    public int GeneratedRecords { get; set; }
    public List<ShiftRotationGenerationResultItemDto> Items { get; set; } = new();
}

public class ShiftRotationGenerationResultItemDto
{
    public string EmployeeId { get; set; } = string.Empty;
    public int GeneratedRecords { get; set; }
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
}
