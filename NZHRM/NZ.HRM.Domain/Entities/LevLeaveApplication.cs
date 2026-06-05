using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("leave_application", Schema = "leave_mgmt")]
    public class LevLeaveApplication : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string LeaveTypeId { get; set; } = string.Empty;
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public decimal TotalDays { get; set; }
        public string? LeaveReason { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public string? WorkflowId { get; set; }
        public string? LeaveStatus { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
        [ForeignKey("LeaveTypeId")] public LevLeaveType? LeaveType { get; set; }

        public ICollection<LevLeaveApplicationDetails> Details { get; set; } = new HashSet<LevLeaveApplicationDetails>();
        public ICollection<LevLeaveApprovalHistory> ApprovalHistory { get; set; } = new HashSet<LevLeaveApprovalHistory>();
    }
}
