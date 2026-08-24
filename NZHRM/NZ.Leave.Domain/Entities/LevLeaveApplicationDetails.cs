using System.ComponentModel.DataAnnotations.Schema;
using NZ.Shared.Domain.Common;

namespace NZ.Leave.Domain.Entities
{
    [Table("leave_application_details", Schema = "leave_mgmt")]
    public class LevLeaveApplicationDetails : BaseEntityWithSortOrder
    {
        public string LeaveApplicationId { get; set; } = string.Empty;
        public DateOnly LeaveDate { get; set; }
        public decimal LeaveFraction { get; set; }
        public string? LeaveDayType { get; set; }

        [ForeignKey("LeaveApplicationId")] public LevLeaveApplication? LeaveApplication { get; set; }
    }
}
