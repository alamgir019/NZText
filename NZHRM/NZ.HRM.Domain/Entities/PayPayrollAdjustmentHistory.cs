using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.Shared.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("payroll_adjustment_history", Schema = "payroll")]
    public class PayPayrollAdjustmentHistory : BaseEntityWithSortOrder
    {
        public string PayrollAdjustmentId { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty; // CREATED, UPDATED, SUBMITTED, CANCELLED, APPROVED, REJECTED
        public string? PerformedBy { get; set; }
        public DateTime PerformedOn { get; set; }
        public string? Notes { get; set; }
        public string? OldData { get; set; }
        public string? NewData { get; set; }

        [ForeignKey("PayrollAdjustmentId")]
        public PayPayrollAdjustment? PayrollAdjustment { get; set; }
    }
}
