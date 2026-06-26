using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("mst_department_unit_complex", Schema = "master")]
    public class MstDepartmentUnitComplex : BaseEntity
    {
        public string DepartmentId { get; set; } = string.Empty;
        public string UnitId { get; set; } = string.Empty;
        public string ComplexId { get; set; } = string.Empty;

        [ForeignKey("DepartmentId")] public MstDepartment? Department { get; set; }
        [ForeignKey("UnitId")] public MstUnit? Unit { get; set; }
        [ForeignKey("ComplexId")] public MstGroupComplex? Complex { get; set; }
    }
}
