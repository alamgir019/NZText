using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("bank", Schema = "lookup")]
    public class Bank : BaseEntityWithSortOrder
    {
        public string BankCode { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
        public string? RoutingNo { get; set; }
        public bool ActiveFlag { get; set; } = true;
    }
}
