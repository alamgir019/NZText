using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("gender", Schema = "lookup")]
    public class Gender : BaseEntityWithSortOrder
    {
        public string GenderCode { get; set; } = string.Empty;
        public string GenderName { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
        public bool ActiveFlag { get; set; } = true;
    }
}
