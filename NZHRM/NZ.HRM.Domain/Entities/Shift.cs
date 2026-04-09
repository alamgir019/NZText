namespace NZ.HRM.Domain.Entities
{
    public class Shift
    {
        public int ShiftId { get; set; }
        public string ShiftName { get; set; } = string.Empty;
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
