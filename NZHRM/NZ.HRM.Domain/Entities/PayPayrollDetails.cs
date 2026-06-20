using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("payroll_details", Schema = "payroll")]
    public class PayPayrollDetails : BaseEntityWithSortOrder
    {
        public string PayrollId { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;
        public decimal GrossSalary { get; set; }
        public decimal PayableDays { get; set; }
        public decimal WorkedDays { get; set; }
        public decimal OTAmount { get; set; }
        public decimal BonusAmount { get; set; }
        public decimal ArrearAmount { get; set; }
        public decimal DeductionAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal LoanRecovery { get; set; }
        public decimal NetSalary { get; set; }

        [ForeignKey("PayrollId")] public PayPayrollHeader? Payroll { get; set; }
        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
    }
}
