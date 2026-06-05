using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("payslip", Schema = "payroll")]
    public class PayPayslip : BaseEntityWithSortOrder
    {
        public string PayrollDetailId { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;
        public string PayrollMonth { get; set; } = string.Empty;
        public string? PayslipFilePath { get; set; }
        public DateTime? GeneratedDate { get; set; }
        public string? GeneratedBy { get; set; }

        [ForeignKey("PayrollDetailId")] public PayPayrollDetails? PayrollDetail { get; set; }
        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
    }
}
