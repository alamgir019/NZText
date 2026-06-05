using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("leave_policy", Schema = "leave_mgmt")]
    public class LevLeavePolicy : BaseEntityWithSortOrder
    {
        public string LeaveTypeId { get; set; } = string.Empty;
        public string? EmployeeCategoryId { get; set; }
        public decimal AnnualEntitlement { get; set; }
        public bool CarryForwardAllowed { get; set; }
        public decimal MaxCarryForwardDays { get; set; }
        public bool EncashAllowed { get; set; }
        public int ApprovalLevels { get; set; }
        public DateOnly? EffectiveDate { get; set; }

        [ForeignKey("LeaveTypeId")] public LevLeaveType? LeaveType { get; set; }
    }
}
