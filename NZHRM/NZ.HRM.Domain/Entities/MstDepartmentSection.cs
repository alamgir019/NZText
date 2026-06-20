using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("mst_department_section", Schema = "master")]
    public class MstDepartmentSection : BaseEntityWithSortOrder
    {
        public string DepartmentId { get; set; } = string.Empty;
        public string SectionId { get; set; } = string.Empty;

        [ForeignKey("DepartmentId")] public MstDepartment? Department { get; set; }
        [ForeignKey("SectionId")] public MstSection? Section { get; set; }
    }
}
