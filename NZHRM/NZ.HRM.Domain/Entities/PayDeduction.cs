using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("deduction", Schema = "payroll")]
    public class PayDeduction : BaseEntityWithSortOrder
    {
        public string PayrollDetailId { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;
        public string? DeductionType { get; set; }
        public decimal DeductionAmount { get; set; }
        public string? Remarks { get; set; }

        [ForeignKey("PayrollDetailId")] public PayPayrollDetails? PayrollDetail { get; set; }
        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
    }
}
