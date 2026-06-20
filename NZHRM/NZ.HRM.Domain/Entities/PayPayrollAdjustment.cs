using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("payroll_adjustment", Schema = "payroll")]
    public class PayPayrollAdjustment : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string PayrollMonth { get; set; } = string.Empty;
        public string? AdjustmentType { get; set; }
        public decimal? OldAmount { get; set; }
        public decimal? NewAmount { get; set; }
        public string? Reason { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? AdjustmentDate { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
    }
}
