using NZ.HRM.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZ.HRM.Domain.Entities
{
    [Table("division", Schema = "lookup")]
    public class Division : BaseEntityWithSortOrder
    {
        public string DivisionName { get; set; } = string.Empty;

        // Navigation property
        public ICollection<District>? Districts { get; set; }
    }
}
