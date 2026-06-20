using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("leave_type", Schema = "leave_mgmt")]
    public class LevLeaveType : BaseEntityWithSortOrder
    {
        [MaxLength(20)]
        public string LeaveCode { get; set; } = string.Empty;
        [MaxLength(100)]
        public string LeaveName { get; set; } = string.Empty;
        public string? LeaveCategory { get; set; }
        public decimal AnnualEntitlement { get; set; }
        public bool Encashable { get; set; }
        public bool CarryForwardAllowed { get; set; }
        public decimal MaxCarryForwardDays { get; set; }
        public bool ApprovalRequired { get; set; }
        public bool Status { get; set; }

        public ICollection<LevLeaveBalance> Balances { get; set; } = new HashSet<LevLeaveBalance>();
        public ICollection<LevLeaveApplication> Applications { get; set; } = new HashSet<LevLeaveApplication>();
        public ICollection<LevLeavePolicy> Policies { get; set; } = new HashSet<LevLeavePolicy>();
    }
}
