namespace NZ.Leave.Application.LeaveRequests.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommand
    {
        public string RequestId { get; set; } = string.Empty;
        public string LeaveType { get; set; } = string.Empty;
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string? ForwardedBy { get; set; }
        public DateOnly? ForwardedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
