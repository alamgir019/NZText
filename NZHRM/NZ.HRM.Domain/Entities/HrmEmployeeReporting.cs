using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("employee_reporting", Schema = "hrm")]
    public class HrmEmployeeReporting : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string ReportingEmployeeId { get; set; } = string.Empty;
        public string? ReportingType { get; set; }
        public DateOnly? EffectiveFrom { get; set; }
        public DateOnly? EffectiveTo { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
        [ForeignKey("ReportingEmployeeId")] public HrmEmployeeMaster? ReportingEmployee { get; set; }
    }
}
