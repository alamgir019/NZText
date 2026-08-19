using System.ComponentModel.DataAnnotations.Schema;
using NZ.Shared.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("processing_log", Schema = "attendance")]
    public class AttProcessingLog : BaseEntityWithSortOrder
    {
        public DateOnly ProcessDate { get; set; }
        public int TotalEmployees { get; set; }
        public int TotalProcessed { get; set; }
        public int TotalExceptions { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? ProcessedBy { get; set; }
    }
}
