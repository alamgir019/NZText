using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("ot_details", Schema = "payroll")]
    public class PayOtDetails : BaseEntityWithSortOrder
    {
        public string PayrollDetailId { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;
        public string PayrollMonth { get; set; } = string.Empty;
        public decimal TotalOTHours { get; set; }
        public decimal OTRate { get; set; }
        public decimal OTAmount { get; set; }

        [ForeignKey("PayrollDetailId")] public PayPayrollDetails? PayrollDetail { get; set; }
        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
    }
}
