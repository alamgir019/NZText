using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("tax", Schema = "payroll")]
    public class PayTax : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string PayrollMonth { get; set; } = string.Empty;
        public decimal TaxableIncome { get; set; }
        public decimal TaxAmount { get; set; }
        public string? TaxRuleId { get; set; }
        public DateTime? CalculationDate { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
    }
}
