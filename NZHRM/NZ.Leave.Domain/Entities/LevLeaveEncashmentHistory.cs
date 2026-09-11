using NZ.Shared.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZ.Leave.Domain.Entities
{
    [Table("leave_encashment_history", Schema = "leave_mgmt")]
    public class LevLeaveEncashmentHistory : BaseEntity
    {
        public string EncashmentId { get; set; } = string.Empty;
        public int WorkflowStepNo { get; set; }
        public string? ApproverId { get; set; }
        public string? ActionTaken { get; set; }
        public string? Remarks { get; set; }

        [ForeignKey("EncashmentId")] public LevLeaveEncashment? Encashment { get; set; }
    }
}
