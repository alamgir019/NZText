using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("marital_status", Schema = "lookup")]
    public class MaritalStatus : BaseEntityWithSortOrder
    {
        public string StatusCode { get; set; } = string.Empty;
        public string StatusName { get; set; } = string.Empty;
        public bool ActiveFlag { get; set; } = true;
    }
}
