namespace NZ.HRM.Domain.Entities
{
    public class Section
    {
        public int SectionId { get; set; }
        public int DepartmentId { get; set; }
        public string SectionName { get; set; } = string.Empty;

        public Department? Department { get; set; }
    }
}
