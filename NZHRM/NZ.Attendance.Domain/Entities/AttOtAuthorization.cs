using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.Shared.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("ot_authorization", Schema = "attendance")]
    public class AttOtAuthorization : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public DateOnly OtDate { get; set; }
        public TimeSpan? ApprovedStartTime { get; set; }
        public TimeSpan? ApprovedEndTime { get; set; }
        public decimal? ApprovedHours { get; set; }
        public string? RequestedBy { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string? Status { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
    }
}
