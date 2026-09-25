namespace NZ.Leave.Application.LeaveEncashmentRequests.Dto
{
    public class LeaveEncashmentRequestDetailDto
    {
        public string RequestId { get; set; } = string.Empty;
        public string RequestType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime? AppliedOn { get; set; }
        public LeaveEncashmentEmployeeDto Employee { get; set; } = new();
        public LeaveEncashmentWorkflowDto Workflow { get; set; } = new();
        public LeaveEncashmentDetailsDto EncashmentDetails { get; set; } = new();
        public LeaveEncashmentEarnedLeaveInfoDto? EarnedLeaveInfo { get; set; }
        public LeaveEncashmentMaternityLeaveInfoDto? MaternityLeaveInfo { get; set; }
        public List<LeaveEncashmentAttachmentDto> Attachments { get; set; } = new();
        public List<string> ImportantRules { get; set; } = new();
    }

    public class LeaveEncashmentEmployeeDto
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public string? Department { get; set; }
        public string? Designation { get; set; }
        public DateOnly? DateOfJoining { get; set; }
        public LeaveEncashmentReportingManagerDto? ReportingManager { get; set; }
    }

    public class LeaveEncashmentReportingManagerDto
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
    }

    public class LeaveEncashmentWorkflowDto
    {
        public string? ForwardedByDepartment { get; set; }
        public string? ForwardedBySection { get; set; }
        public DateTime? ForwardedOn { get; set; }
    }

    public class LeaveEncashmentDetailsDto
    {
        public decimal EncashmentDaysRequested { get; set; }
        public string EncashmentRate { get; set; } = string.Empty;
        public decimal EstimatedAmount { get; set; }
        public string? RemarksByEmployee { get; set; }
    }

    public class LeaveEncashmentEarnedLeaveInfoDto
    {
        public decimal TotalEarnedLeaveCredited { get; set; }
        public decimal EarnedLeaveUtilized { get; set; }
        public decimal BalanceEarnedLeave { get; set; }
        public decimal EligibleForEncashment { get; set; }
    }

    public class LeaveEncashmentMaternityLeaveInfoDto
    {
        public string RequestPart { get; set; } = string.Empty;
        public int MaternityLeaveEntitlement { get; set; }
        public string LeaveStructure { get; set; } = string.Empty;
        public DateOnly? ExpectedDeliveryDate { get; set; }
        public DateOnly? MaternityLeaveStartDate { get; set; }
        public DateOnly? MaternityLeaveEndDate { get; set; }
        public decimal EncashmentDaysRequested { get; set; }
        public string? RemarksByEmployee { get; set; }
        public string? Note { get; set; }
    }

    public class LeaveEncashmentAttachmentDto
    {
        public string AttachmentId { get; set; } = string.Empty;
        public string? FileName { get; set; }
        public long FileSizeKB { get; set; }
        public DateTime? UploadedOn { get; set; }
        public string DownloadUrl { get; set; } = string.Empty;
    }
}