namespace NZ.Leave.Application.LeaveTypes.Dto
{
    public class LeaveTypeDto
    {
        public string Id { get; set; } = string.Empty;
        public string LeaveCode { get; set; } = string.Empty;
        public string LeaveName { get; set; } = string.Empty;
        public string? LeaveCategory { get; set; }
        public decimal AnnualEntitlement { get; set; }
        public bool Encashable { get; set; }
        public bool CarryForwardAllowed { get; set; }
        public decimal MaxCarryForwardDays { get; set; }
        public bool ApprovalRequired { get; set; }
        public bool Status { get; set; }
    }
}
