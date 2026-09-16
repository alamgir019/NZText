namespace NZ.Leave.Application.LeaveEncashmentRequests.Queries.GetLeaveEncashmentRequests
{
    public class GetLeaveEncashmentRequestsQuery
    {
        public string? Status { get; set; }
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 20;
        public string? Instalment { get; set; }
        public string? LeaveType { get; set; }
    }
}
