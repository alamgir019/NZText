using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.Shared.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("ot_balance", Schema = "attendance")]
    public class AttOtBalance : BaseEntity
    {
        public string EmployeeId { get; set; } = string.Empty;
        // Balance stored in hours (decimal) to allow fractional hours
        public decimal BalanceHours { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
    }
}
