namespace NZ.HRM.Domain.Entities
{
    public class CompanyLocation : NZ.HRM.Domain.Common.BaseEntity
    {
        public string CompanyId { get; set; } = string.Empty;
        public Company? Company { get; set; }

        public string LocationId { get; set; } = string.Empty;
        public Location? Location { get; set; }
    }
}
