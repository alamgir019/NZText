namespace NZ.Attendance.Application.OvertimeRequests.Commands.ApproveOvertimeEmployee
{
    public class ApproveOvertimeEmployeeCommand
    {
        public string ItemId { get; set; } = string.Empty;
        public string ApprovedBy { get; set; } = string.Empty;
        public bool Approved { get; set; } = true;
    }
}
