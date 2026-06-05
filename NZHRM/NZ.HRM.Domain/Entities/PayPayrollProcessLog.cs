using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("payroll_process_log", Schema = "payroll")]
    public class PayPayrollProcessLog : BaseEntityWithSortOrder
    {
        public string PayrollMonth { get; set; } = string.Empty;
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int EmployeeCount { get; set; }
        public int ProcessedCount { get; set; }
        public int ExceptionCount { get; set; }
        public string? ProcessedBy { get; set; }
    }
}
