namespace NZ.Leave.Application.LeaveRequests.Commands.DeleteLeaveRequest
{
    public class DeleteLeaveRequestCommand
    {
        public string RequestId { get; set; } = string.Empty;
    }

    public class DeleteLeaveRequestResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? ErrorCode { get; set; }
    }
}
