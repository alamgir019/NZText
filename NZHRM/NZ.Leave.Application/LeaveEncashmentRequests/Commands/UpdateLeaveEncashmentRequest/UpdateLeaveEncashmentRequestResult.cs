namespace NZ.Leave.Application.LeaveEncashmentRequests.Commands.UpdateLeaveEncashmentRequest
{
    public class UpdateLeaveEncashmentRequestResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? ErrorCode { get; set; }
    }
}
