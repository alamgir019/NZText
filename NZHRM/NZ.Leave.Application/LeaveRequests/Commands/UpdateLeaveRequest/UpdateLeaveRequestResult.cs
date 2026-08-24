namespace NZ.Leave.Application.LeaveRequests.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? ErrorCode { get; set; }
    }
}
