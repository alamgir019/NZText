using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    public class Shift : BaseEntity
    {
        public string ShiftName { get; set; } = string.Empty;
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
