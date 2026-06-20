using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("special_payroll_band", Schema = "payroll")]
    public class PaySpecialPayrollBand : BaseEntityWithSortOrder
    {
        public string PolicyId { get; set; } = string.Empty;
        public string BandName { get; set; } = string.Empty;
        public decimal FromPercentage { get; set; }
        public decimal ToPercentage { get; set; }

        [ForeignKey("PolicyId")] public PaySpecialPayrollPolicy? Policy { get; set; }
    }
}
