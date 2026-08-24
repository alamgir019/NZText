namespace NZ.Leave.Application.LeaveRequests.Queries.GetLeaveRequests
{
    public class GetLeaveRequestsQuery
    {
        public string? Status { get; set; }
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 20;
    }
}
