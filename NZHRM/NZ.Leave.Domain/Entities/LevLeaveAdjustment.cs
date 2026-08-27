using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Entities;
using NZ.Shared.Domain.Common;

namespace NZ.Leave.Domain.Entities
{
    [Table("leave_adjustment", Schema = "leave_mgmt")]
    public class LevLeaveAdjustment : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string LeaveTypeId { get; set; } = string.Empty;
        public DateOnly AdjustmentDate { get; set; }
        public decimal AdjustmentDays { get; set; }
        public string? AdjustmentReason { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
        [ForeignKey("LeaveTypeId")] public LevLeaveType? LeaveType { get; set; }
    }
}
