using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("manpower_requisition", Schema = "recruitment")]
    public class RecManpowerRequisition : BaseEntityWithSortOrder
    {
        public string RequisitionCode { get; set; } = string.Empty;
        public string? PositionId { get; set; }
        public int VacancyCount { get; set; }
        public string? RequisitionReason { get; set; }
        public string? RequestedBy { get; set; }
        public DateTime? RequestedDate { get; set; }

        [ForeignKey("PositionId")] public RecJobPosition? JobPosition { get; set; }
    }
}
