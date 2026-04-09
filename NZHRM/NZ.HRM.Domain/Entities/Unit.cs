namespace NZ.HRM.Domain.Entities
{
    public class Unit
    {
        public int UnitId { get; set; }
        public int CompanyId { get; set; }
        public string UnitName { get; set; } = string.Empty;
        public int LocationId { get; set; }

        public Company? Company { get; set; }
        public Location? Location { get; set; }
    }
}
