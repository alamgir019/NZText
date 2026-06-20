using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("blood_group", Schema = "lookup")]
    public class BloodGroup : BaseEntityWithSortOrder
    {
        public string BloodGroupCode { get; set; } = string.Empty;
        public string BloodGroupName { get; set; } = string.Empty;
        public bool ActiveFlag { get; set; } = true;
    }
}
