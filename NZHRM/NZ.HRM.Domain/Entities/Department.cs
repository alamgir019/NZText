using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    public class Department : BaseEntityWithSortOrder
    {
        public string DepartmentName { get; set; } = string.Empty;
        public string DepartmentCode { get; set; } = string.Empty;
        public ICollection<DepartmentSection>? DepartmentSections { get; set; }
        public ICollection<LocationDepartment>? LocationDepartments { get; set; }
    }
}
