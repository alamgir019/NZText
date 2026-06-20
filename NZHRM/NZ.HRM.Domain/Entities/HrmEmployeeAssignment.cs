using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("employee_assignment", Schema = "hrm")]
    public class HrmEmployeeAssignment : BaseEntityWithSortOrder
    {
        // FK to employee_master.Id
        public string EmployeeId { get; set; } = string.Empty;

        // Assigned organization
        public string? AssignedGroupComplexId { get; set; }
        public string? AssignedUnitId { get; set; }
        public string? AssignedDepartmentId { get; set; }

        public DateOnly EffectiveFrom { get; set; }
        public DateOnly? EffectiveTo { get; set; }

        public string? Remarks { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
        [ForeignKey("AssignedGroupComplexId")] public MstGroupComplex? GroupComplex { get; set; }
        [ForeignKey("AssignedUnitId")] public MstUnit? AssignedUnit { get; set; }
        [ForeignKey("AssignedDepartmentId")] public MstDepartment? AssignedDepartment { get; set; }
    }
}
