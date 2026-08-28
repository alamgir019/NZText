namespace NZ.Leave.Application.LeaveEncashmentRequests.Commands.CreateLeaveEncashmentRequests
{
    public class CreateLeaveEncashmentRequestsResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public int TotalEmployees { get; set; }
        public decimal TotalDays { get; set; }
        public string? ErrorCode { get; set; }
    }
}
