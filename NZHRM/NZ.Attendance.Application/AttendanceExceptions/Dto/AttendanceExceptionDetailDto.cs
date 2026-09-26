namespace NZ.Attendance.Application.AttendanceExceptions.Dto
{
    public class AttendanceExceptionEmployeeInfoDto
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public string? Department { get; set; }
        public string? Designation { get; set; }
        public DateOnly? DateOfJoining { get; set; }
        public AttendanceExceptionReportingManagerDto? ReportingManager { get; set; }
    }

    public class AttendanceExceptionReportingManagerDto
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
    }

    public class AttendanceExceptionWorkflowInfoDto
    {
        public string? ForwardedByDepartment { get; set; }
        public string? ForwardedBySection { get; set; }
        public DateTime? ForwardedOn { get; set; }
        public string CurrentStatus { get; set; } = string.Empty;
    }

    public class AttendanceExceptionInformationDto
    {
        public string ExceptionType { get; set; } = string.Empty;
        public DateOnly ExceptionDate { get; set; }
        public string? ShiftName { get; set; }
        public string? ShiftTime { get; set; }
        public string? ScheduledInTime { get; set; }
        public string? ActualInTime { get; set; }
        public string? ScheduledOutTime { get; set; }
        public string? ActualOutTime { get; set; }
        public string? ReasonProvided { get; set; }
        public string? RemarksByFloor { get; set; }
    }

    public class AttendanceExceptionAttachmentDto
    {
        public string AttachmentId { get; set; } = string.Empty;
        public string? FileName { get; set; }
        public DateTime? UploadedOn { get; set; }
        public string DownloadUrl { get; set; } = string.Empty;
    }

    public class AttendanceExceptionDetailDto
    {
        public string RequestId { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateOnly ExceptionDate { get; set; }
        public string? Shift { get; set; }
        public AttendanceExceptionEmployeeInfoDto Employee { get; set; } = new();
        public AttendanceExceptionWorkflowInfoDto Workflow { get; set; } = new();
        public AttendanceExceptionInformationDto ExceptionInformation { get; set; } = new();
        public List<AttendanceExceptionAttachmentDto> Attachments { get; set; } = new();
    }
}
