using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Entities;
using NZ.Shared.Domain.Common;

namespace NZ.Leave.Domain.Entities
{
    [Table("leave_encashment", Schema = "leave_mgmt")]
    public class LevLeaveEncashment : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string LeaveTypeId { get; set; } = string.Empty;
        public decimal EncashDays { get; set; }
        public decimal EncashAmount { get; set; }
        public string? PayrollMonth { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? EncashDate { get; set; }
        public string? Reason { get; set; }
        public string? Status { get; set; }
        public string? ForwardedBy { get; set; }
        public DateTime? ForwardedDate { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
        [ForeignKey("LeaveTypeId")] public LevLeaveType? LeaveType { get; set; }
    }
}
