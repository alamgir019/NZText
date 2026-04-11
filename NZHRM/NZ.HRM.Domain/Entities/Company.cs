namespace NZ.HRM.Domain.Entities
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyCode { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public int LocationId { get; set; }
        public bool ActiveStatus { get; set; }

        public Location? Location { get; set; }
        public ICollection<Unit>? Units { get; set; }
    }
}
