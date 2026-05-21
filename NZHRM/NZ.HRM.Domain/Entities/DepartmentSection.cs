namespace NZ.HRM.Domain.Entities
{
    public class DepartmentSection : Common.BaseEntity
    {
        public string DepartmentId { get; set; } = string.Empty;
        public Department? Department { get; set; }

        public string SectionId { get; set; } = string.Empty;
        public Section? Section { get; set; }
    }
}
