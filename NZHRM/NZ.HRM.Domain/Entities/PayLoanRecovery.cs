using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("loan_recovery", Schema = "payroll")]
    public class PayLoanRecovery : BaseEntityWithSortOrder
    {
        public string PayrollDetailId { get; set; } = string.Empty;
        public string LoanId { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;
        public decimal RecoveryAmount { get; set; }
        public decimal BalanceAfterRecovery { get; set; }

        [ForeignKey("PayrollDetailId")] public PayPayrollDetails? PayrollDetail { get; set; }
        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
    }
}
