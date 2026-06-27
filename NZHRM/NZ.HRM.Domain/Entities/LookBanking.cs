using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("banking", Schema = "lookup")]
    public class LookBanking : BaseEntityWithSortOrder
    {
        public string BankingCode { get; set; } = string.Empty;
        public string BankingName { get; set; } = string.Empty;
        public bool MobileBankingFlag { get; set; }
    }
}
