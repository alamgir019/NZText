namespace NZ.Leave.Application.LeaveEncashmentRequests.Commands.UpdateLeaveEncashmentRequest
{
    public class UpdateLeaveEncashmentRequestCommand
    {
        public string RequestId { get; set; } = string.Empty;
        public string LeaveType { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public DateOnly EncashDate { get; set; }
        public decimal EncashDays { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string? ForwardedBy { get; set; }
        public DateOnly? ForwardedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
