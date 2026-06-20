using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("increment_history", Schema = "payroll")]
    public class PayIncrementHistory : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public DateOnly? EffectiveDate { get; set; }
        public decimal? OldGrossSalary { get; set; }
        public decimal? NewGrossSalary { get; set; }
        public decimal? IncrementAmount { get; set; }
        public decimal? IncrementPercent { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
    }
}
