using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("job_position", Schema = "recruitment")]
    public class RecJobPosition : BaseEntityWithSortOrder
    {
        public string PositionCode { get; set; } = string.Empty;
        public string PositionName { get; set; } = string.Empty;

        public ICollection<RecCandidate> Candidates { get; set; } = new HashSet<RecCandidate>();
    }
}
