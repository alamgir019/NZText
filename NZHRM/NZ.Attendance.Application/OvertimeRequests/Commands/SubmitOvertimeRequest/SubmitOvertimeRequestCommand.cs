namespace NZ.Attendance.Application.OvertimeRequests.Commands.SubmitOvertimeRequest
{
    public class SubmitOvertimeRequestCommand
    {
        public string OvertimeRequestId { get; set; } = string.Empty;
        public string SubmittedBy { get; set; } = string.Empty;
    }
}
