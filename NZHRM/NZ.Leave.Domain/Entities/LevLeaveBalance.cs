using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Entities;
using NZ.Shared.Domain.Common;


namespace NZ.Leave.Domain.Entities
{
    [Table("leave_balance", Schema = "leave_mgmt")]
    public class LevLeaveBalance : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string LeaveTypeId { get; set; } = string.Empty;
        public string YearId { get; set; } = string.Empty;
        public decimal OpeningBalance { get; set; }
        public decimal EarnedLeave { get; set; }
        public decimal AvailedLeave { get; set; }
        public decimal AdjustedLeave { get; set; }
        public decimal EncashedLeave { get; set; }
        public decimal ClosingBalance { get; set; }
        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
        [ForeignKey("LeaveTypeId")] public LevLeaveType? LeaveType { get; set; }
    }
}
