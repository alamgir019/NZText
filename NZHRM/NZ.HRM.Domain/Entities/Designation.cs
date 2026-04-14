using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    public class Designation: BaseEntity
    {
        public string DesignationName { get; set; } = string.Empty;
        public string DesignationCode { get; set; } = string.Empty;
        public ICollection<Requisition>? Requisitions { get; set; }
    }
}
