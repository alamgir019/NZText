using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("payroll_lock", Schema = "payroll")]
    public class PayPayrollLock : BaseEntityWithSortOrder
    {
        public string PayrollMonth { get; set; } = string.Empty;
        public string? UnitId { get; set; }
        public string? LockedBy { get; set; }
        public DateTime? LockDate { get; set; }
        public string? UnlockBy { get; set; }
        public DateTime? UnlockDate { get; set; }
        public string? Status { get; set; }
    }
}
