using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("holiday_calendar", Schema = "master")]
    public class MstHolidayCalendar : BaseEntityWithSortOrder
    {
        public DateOnly HolidayDate { get; set; }
        public string HolidayName { get; set; } = string.Empty;
        public string? HolidayType { get; set; }
        public string? UnitId { get; set; }
        public bool Status { get; set; }
    }
}
