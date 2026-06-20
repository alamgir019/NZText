using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("export_history", Schema = "audit")]
    public class AudExportHistory : BaseEntityWithSortOrder
    {
        public string UserId { get; set; } = string.Empty;
        public string? ModuleName { get; set; }
        public string? ExportType { get; set; }
        public DateTime? ExportDateTime { get; set; }
        public int RecordCount { get; set; }

        [ForeignKey("UserId")] public SecUser? User { get; set; }
    }
}
