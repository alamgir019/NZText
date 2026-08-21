namespace NZ.Attendance.Application.OvertimeRequests.Commands.ApproveOvertimeRequest
{
    public class ApproveOvertimeRequestCommand
    {
        public string OvertimeRequestId { get; set; } = string.Empty;
        public string ApprovedBy { get; set; } = string.Empty;
        public bool Approved { get; set; } = true;
    }
}
