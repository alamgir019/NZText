namespace NZ.Leave.Application.LeaveRequests.Commands.CreateLeaveRequests
{
    public class CreateLeaveRequestsResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public int TotalEmployees { get; set; }
        public decimal TotalDays { get; set; }
        public string? ErrorCode { get; set; }
    }
}
