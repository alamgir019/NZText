namespace NZ.Leave.Application.LeaveEncashmentRequests.Dto
{
    public class LeaveEncashmentRequestDto
    {
        public string RequestId { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public string LeaveType { get; set; } = string.Empty;
        public DateOnly EncashDate { get; set; }
        public decimal EncashDays { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ForwardedBy { get; set; }
        public DateTime? ForwardedDate { get; set; }
    }
}
