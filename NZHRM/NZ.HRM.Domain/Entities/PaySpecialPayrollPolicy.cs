using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("special_payroll_policy", Schema = "payroll")]
    public class PaySpecialPayrollPolicy : BaseEntityWithSortOrder
    {
        public string PolicyCode { get; set; } = string.Empty;
        public string PolicyName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal? PercentageOfSalary { get; set; }
        public bool ActiveFlag { get; set; }
    }
}
