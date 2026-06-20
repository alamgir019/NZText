using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("payroll_header", Schema = "payroll")]
    public class PayPayrollHeader : BaseEntityWithSortOrder
    {
        public string PayrollMonth { get; set; } = string.Empty;
        public string? GroupId { get; set; }
        public string? UnitId { get; set; }
        public string? PayrollStatus { get; set; }
        public int TotalEmployees { get; set; }
        public decimal TotalGross { get; set; }
        public decimal TotalDeduction { get; set; }
        public decimal TotalNetSalary { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }

        public ICollection<PayPayrollDetails> Details { get; set; } = new HashSet<PayPayrollDetails>();
    }
}
