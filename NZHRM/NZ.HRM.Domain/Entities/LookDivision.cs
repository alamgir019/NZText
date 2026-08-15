using NZ.HRM.Domain.Common;
using NZ.Shared.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZ.HRM.Domain.Entities
{
    [Table("division", Schema = "lookup")]
    public class LookDivision : BaseEntityWithSortOrder
    {
        public string DivisionName { get; set; } = string.Empty;
        public string? DivisionNameBangla { get; set; }

        // Navigation property
        public ICollection<LookDistrict>? Districts { get; set; }
    }
}
