using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("leave_cancellation", Schema = "leave_mgmt")]
    public class LevLeaveCancellation : BaseEntityWithSortOrder
    {
        public string LeaveApplicationId { get; set; } = string.Empty;
        public DateTime? CancellationDate { get; set; }
        public string? CancelledBy { get; set; }
        public string? Reason { get; set; }
        public string? ApprovedBy { get; set; }

        [ForeignKey("LeaveApplicationId")] public LevLeaveApplication? LeaveApplication { get; set; }
    }
}
