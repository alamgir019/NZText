namespace NZ.Payroll.Application.PayrollExceptions.DTOs;

public class PayrollExceptionRequestDetailDto
{
    public string RequestId { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateOnly? AdjustmentDate { get; set; }
    public string? Shift { get; set; }
    public PayrollExceptionRequestDetailEmployeeDto Employee { get; set; } = new();
    public PayrollExceptionRequestAdjustmentDto Adjustment { get; set; } = new();
    public PayrollExceptionRequestForwardedByDto? ForwardedBy { get; set; }
    public List<PayrollExceptionRequestAttachmentDto> Attachments { get; set; } = new();
}

public class PayrollExceptionRequestDetailEmployeeDto
{
    public string EmployeeId { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
    public string? Department { get; set; }
    public string? Designation { get; set; }
    public DateOnly? DateOfJoining { get; set; }
    public PayrollExceptionRequestReportingManagerDto? ReportingManager { get; set; }
}

public class PayrollExceptionRequestReportingManagerDto
{
    public string EmployeeId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}

public class PayrollExceptionRequestAdjustmentDto
{
    public string? AdjustmentType { get; set; }
    public string? AdjustmentNature { get; set; }
    public string? OriginalOutPunch { get; set; }
    public string? CorrectedOutPunch { get; set; }
    public bool? OtApplicable { get; set; }
    public string? OtType { get; set; }
    public string? OtHours { get; set; }
    public decimal? OtRate { get; set; }
    public bool? ImpactOnPayroll { get; set; }
    public string? ReasonProvided { get; set; }
    public string? RemarksByAttendanceCell { get; set; }
}

public class PayrollExceptionRequestForwardedByDto
{
    public string? Department { get; set; }
    public DateTime? ForwardedDateTime { get; set; }
}

public class PayrollExceptionRequestAttachmentDto
{
    public string AttachmentId { get; set; } = string.Empty;
    public string? FileName { get; set; }
    public long FileSizeKb { get; set; }
    public DateTime? UploadedDateTime { get; set; }
    public string DownloadUrl { get; set; } = string.Empty;
}
