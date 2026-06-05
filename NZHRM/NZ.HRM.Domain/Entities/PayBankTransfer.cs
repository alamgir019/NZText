using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("bank_transfer", Schema = "payroll")]
    public class PayBankTransfer : BaseEntityWithSortOrder
    {
        public string PayrollId { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;
        public string? BankName { get; set; }
        public string? AccountNo { get; set; }
        public decimal TransferAmount { get; set; }
        public string? TransferStatus { get; set; }
        public DateTime? TransferDate { get; set; }

        [ForeignKey("PayrollId")] public PayPayrollHeader? Payroll { get; set; }
        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
    }
}
