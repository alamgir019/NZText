using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("interview_panel", Schema = "recruitment")]
    public class RecInterviewPanel : BaseEntityWithSortOrder
    {
        public string PanelName { get; set; } = string.Empty;
        public ICollection<RecInterviewSchedule> Schedules { get; set; } = new HashSet<RecInterviewSchedule>();
    }
}
