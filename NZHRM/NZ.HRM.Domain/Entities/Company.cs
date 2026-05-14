using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    public class Company: BaseEntity
    {
        public string CompanyCode { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        // Relationship through mapping entity
        public ICollection<CompanyLocation>? CompanyLocations { get; set; }
        public bool IsCompliant { get; set; }
        public ICollection<Requisition>? Requisitions { get; set; }
    }
}
