using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("appointment", Schema = "recruitment")]
    public class RecAppointment : BaseEntityWithSortOrder
    {
        public string CandidateId { get; set; } = string.Empty;
        public DateOnly? AppointmentDate { get; set; }
        public string? EmployeeId { get; set; }

        [ForeignKey("CandidateId")] public RecCandidate? Candidate { get; set; }
    }
}
