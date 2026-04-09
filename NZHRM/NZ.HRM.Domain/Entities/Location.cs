namespace NZ.HRM.Domain.Entities
{
    public class Location
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;

        public ICollection<Company>? Companies { get; set; }
        public ICollection<Unit>? Units { get; set; }
    }
}
