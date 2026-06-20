using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("leave_balance", Schema = "leave_mgmt")]
    public class LevLeaveBalance : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string LeaveTypeId { get; set; } = string.Empty;
        public int YearId { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal EarnedLeave { get; set; }
        public decimal AvailedLeave { get; set; }
        public decimal AdjustedLeave { get; set; }
        public decimal EncashedLeave { get; set; }
        public decimal ClosingBalance { get; set; }
        public DateTime? LastUpdated { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
        [ForeignKey("LeaveTypeId")] public LevLeaveType? LeaveType { get; set; }
    }
}
