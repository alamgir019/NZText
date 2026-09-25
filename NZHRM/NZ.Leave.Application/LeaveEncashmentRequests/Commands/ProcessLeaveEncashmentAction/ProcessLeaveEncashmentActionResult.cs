namespace NZ.Leave.Application.LeaveEncashmentRequests.Commands.ProcessLeaveEncashmentAction
{
    public class ProcessLeaveEncashmentActionResult
    {
        public bool Success { get; set; }
        public string? ErrorCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string RequestId { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}