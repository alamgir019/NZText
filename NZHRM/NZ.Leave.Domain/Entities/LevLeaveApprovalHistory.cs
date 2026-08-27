using System.ComponentModel.DataAnnotations.Schema;
using NZ.Shared.Domain.Common;

namespace NZ.Leave.Domain.Entities
{
    [Table("leave_approval_history", Schema = "leave_mgmt")]
    public class LevLeaveApprovalHistory : BaseEntityWithSortOrder
    {
        public string LeaveApplicationId { get; set; } = string.Empty;
        public int WorkflowStepNo { get; set; }
        public string? ApproverId { get; set; }
        public string? ActionTaken { get; set; }
        public DateTime? ActionDate { get; set; }
        public string? Remarks { get; set; }

        [ForeignKey("LeaveApplicationId")] public LevLeaveApplication? LeaveApplication { get; set; }
    }
}
