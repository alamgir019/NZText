using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("mst_shift", Schema = "master")]
    public class MstShift : BaseEntityWithSortOrder
    {
        public string ShiftCode { get; set; } = string.Empty;
        public string ShiftName { get; set; } = string.Empty;
        public string ShiftType { get; set; } = string.Empty;
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int GraceMinutes { get; set; }
        public int DaysOffset { get; set; }
        public decimal FullDayHours { get; set; }
    }
}
