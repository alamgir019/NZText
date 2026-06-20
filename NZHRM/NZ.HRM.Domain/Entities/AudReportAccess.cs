using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("report_access", Schema = "audit")]
    public class AudReportAccess : BaseEntityWithSortOrder
    {
        public string UserId { get; set; } = string.Empty;
        public string ReportName { get; set; } = string.Empty;
        public DateTime? AccessDateTime { get; set; }
        public bool ExportFlag { get; set; }

        [ForeignKey("UserId")] public SecUser? User { get; set; }
    }
}
