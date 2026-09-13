namespace NZ.Leave.Application.LeaveRequests.Dto
{

    public class LeaveRequestDto
    {
        public string RequestId { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;
        public string EmployeeCode { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public string LeaveType { get; set; } = string.Empty;
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public decimal TotalDays { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string? ForwardedBy { get; set; }
        public DateTime? ForwardedDate { get; set; }
        public string? ApproveStatus { get; set; }

        // New: available leave balances for the employee
        public List<LeaveBalanceDto> AvailableLeaves { get; set; } = new List<LeaveBalanceDto>();
        public string LeaveTypeId { get; set; } = string.Empty;
    }

    public class LeaveBalanceDto
    {
        public string LeaveTypeId { get; set; } = string.Empty;
        public string LeaveTypeName { get; set; } = string.Empty;
        public decimal OpeningBalance { get; set; }
        public decimal EarnedLeave { get; set; }
        public decimal AvailedLeave { get; set; }
        public decimal AdjustedLeave { get; set; }
        public decimal EncashedLeave { get; set; }
        public decimal ClosingBalance { get; set; }
    }
}
