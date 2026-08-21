using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("ot_request_item", Schema = "attendance")]
    public class AttOtRequestItem : BaseEntity
    {
        // Grouping id to represent a request; multiple items can share the same RequestId
        public string RequestId { get; set; } = string.Empty;

        // Request-level fields (duplicated per item to simplify queries)
        public string CurrentShiftId { get; set; } = string.Empty;
        public DateOnly OtDate { get; set; }
        public string DepartmentId { get; set; } = string.Empty;
        public string? Reason { get; set; }

        // Employee-level fields
        public string EmployeeId { get; set; } = string.Empty;
        public TimeSpan OtHours { get; set; }

        // Workflow / approval at item level
        public string? Status { get; set; }
        public string? SubmittedBy { get; set; }
        public DateTime? SubmittedOn { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
    }
}
