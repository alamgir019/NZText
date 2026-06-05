using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("partial_salary_payment", Schema = "payroll")]
    public class PayPartialSalaryPayment : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string PayrollPeriod { get; set; } = string.Empty; // e.g., 2026-06
        public decimal Amount { get; set; }
        public string? Reason { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? Status { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
    }
}
