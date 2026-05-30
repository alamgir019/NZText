using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    public class Grade : BaseEntityWithSortOrder
    {
        public string GradeName { get; set; } = string.Empty;
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public string EmployeeType { get; set; } = string.Empty;
    }
}
