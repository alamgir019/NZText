using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
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
