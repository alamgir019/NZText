using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.Shared.Domain.Common;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Domain.Entities
{
    [Table("shift_roster", Schema = "attendance")]
    public class AttShiftRoster : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string ShiftId { get; set; } = string.Empty;
        public DateOnly RosterDate { get; set; }
        public string? AssignedBy { get; set; }
        public string? Reason { get; set; }
        public bool Status { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
        [ForeignKey("ShiftId")] public MstShift? Shift { get; set; }
    }
}
