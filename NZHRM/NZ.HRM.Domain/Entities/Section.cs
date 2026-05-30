using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    public class Section : BaseEntityWithSortOrder
    {
        public string SectionName { get; set; } = string.Empty;
        public ICollection<DepartmentSection>? DepartmentSections { get; set; }
        public ICollection<SectionCell>? SectionCells { get; set; }
    }
}
