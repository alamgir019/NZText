namespace NZ.HRM.Domain.Entities
{
    public class SectionCell : Common.BaseEntity
    {
        public string SectionId { get; set; } = string.Empty;
        public Section? Section { get; set; }

        public string CellId { get; set; } = string.Empty;
        public Cell? Cell { get; set; }
    }
}
