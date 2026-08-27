namespace NZ.Leave.Application.LeaveRequests.Commands.CreateLeaveRequests
{
    public class CreateLeaveRequestItem
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public string LeaveType { get; set; } = string.Empty;
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string? ForwardedBy { get; set; }
        public DateOnly? ForwardedDate { get; set; }
    }

    public class CreateLeaveRequestsCommand
    {
        public List<CreateLeaveRequestItem> Requests { get; set; } = new List<CreateLeaveRequestItem>();
        public string? CreatedBy { get; set; }
    }
}
