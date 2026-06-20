using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("arrear", Schema = "payroll")]
    public class PayArrear : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string PayrollMonth { get; set; } = string.Empty;
        public string? ArrearType { get; set; }
        public decimal ArrearAmount { get; set; }
        public DateOnly? EffectiveFrom { get; set; }
        public DateOnly? EffectiveTo { get; set; }
        public string? Status { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
    }
}
