namespace NZ.HRM.Domain.Entities
{
    public class LocationDepartment : Common.BaseEntity
    {
        public string LocationId { get; set; } = string.Empty;
        public Location? Location { get; set; }

        public string DepartmentId { get; set; } = string.Empty;
        public Department? Department { get; set; }
    }
}
