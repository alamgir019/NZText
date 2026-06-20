using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("bonus", Schema = "payroll")]
    public class PayBonus : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string? BonusType { get; set; }
        public decimal BonusAmount { get; set; }
        public DateOnly? BonusDate { get; set; }
        public string? PayrollMonth { get; set; }
        public string? ApprovedBy { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
    }
}
