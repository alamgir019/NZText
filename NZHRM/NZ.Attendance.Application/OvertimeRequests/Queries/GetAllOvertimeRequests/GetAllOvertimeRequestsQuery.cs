namespace NZ.Attendance.Application.OvertimeRequests.Queries.GetAllOvertimeRequests
{
    public class GetAllOvertimeRequestsQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string? ShiftId { get; set; }
        public string? DepartmentId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string? Status { get; set; }
    }
}
