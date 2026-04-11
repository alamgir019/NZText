namespace NZ.HRM.Domain.Entities
{
    public class Holiday
    {
        public int HolidayId { get; set; }
        public string HolidayName { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
    }
}
