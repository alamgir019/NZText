using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Entities;
using NZ.Shared.Domain.Common;

namespace NZ.Leave.Domain.Entities
{
    [Table("leave_opening_balance", Schema = "leave_mgmt")]
    public class LevLeaveOpeningBalance : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string LeaveTypeId { get; set; } = string.Empty;
        public string LeaveYear { get; set; } = string.Empty;
        public decimal OpeningDays { get; set; }
        public DateOnly? AllocationDate { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
        [ForeignKey("LeaveTypeId")] public LevLeaveType? LeaveType { get; set; }
    }
}
