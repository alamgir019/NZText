namespace NZ.Leave.Application.LeaveEncashmentRequests.Commands.DeleteLeaveEncashmentRequest
{
    public class DeleteLeaveEncashmentRequestCommand
    {
        public string RequestId { get; set; } = string.Empty;
    }

    public class DeleteLeaveEncashmentRequestResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? ErrorCode { get; set; }
    }
}
