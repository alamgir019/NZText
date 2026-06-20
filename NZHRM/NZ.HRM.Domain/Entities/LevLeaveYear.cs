using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("leave_year", Schema = "leave_mgmt")]
    public class LevLeaveYear : BaseEntityWithSortOrder
    {
        public int LeaveYearValue { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public bool IsCurrentYear { get; set; }
    }
}
