using System.ComponentModel.DataAnnotations.Schema;
using NZ.Shared.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("weekly_off_pattern", Schema = "attendance")]
    public class AttWeeklyOffPattern : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string DayOfWeek { get; set; } = string.Empty;
        public DateOnly? EffectiveDate { get; set; }
        public bool Status { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
    }
}
