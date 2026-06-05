using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("mst_subunit_department", Schema = "master")]
    public class MstSubunitDepartment : BaseEntityWithSortOrder
    {
        public string SubunitId { get; set; } = string.Empty;
        public string DepartmentId { get; set; } = string.Empty;

        [ForeignKey("SubunitId")] public MstSubunit? Subunit { get; set; }
        [ForeignKey("DepartmentId")] public MstDepartment? Department { get; set; }
    }
}
