using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("payroll_exception", Schema = "payroll")]
    public class PayPayrollException : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string PayrollPeriod { get; set; } = string.Empty;
        public string? ExceptionType { get; set; }
        public string? ExceptionDescription { get; set; }
        public string? Status { get; set; }
        public string? ResolvedBy { get; set; }
        public DateTime? ResolvedDate { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
    }
}
