using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("religion", Schema = "lookup")]
    public class Religion : BaseEntityWithSortOrder
    {
        public string ReligionCode { get; set; } = string.Empty;
        public string ReligionName { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
        public bool ActiveFlag { get; set; } = true;
    }
}
