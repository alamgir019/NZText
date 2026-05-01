using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    public class Department : BaseEntity
    {
        public string DepartmentName { get; set; } = string.Empty;
        public string DepartmentCode { get; set; } = string.Empty;
    }
}
