using NZ.HRM.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZ.HRM.Domain.Entities
{
    public class Section : BaseEntity
    {
        public string DepartmentId { get; set; } = string.Empty;
        [ForeignKey(nameof(DepartmentId))]
        public Department? Department { get; set; }

        public string SectionName { get; set; } = string.Empty;
    }
}
