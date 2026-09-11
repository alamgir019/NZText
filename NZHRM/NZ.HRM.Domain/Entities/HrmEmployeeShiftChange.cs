using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities;

[Table("employee_shift_change", Schema = "hrm")]
public class HrmEmployeeShiftChange : BaseEntity
{
	[Required]
	public string EmployeeId { get; set; } = string.Empty;

	[Required]
	public string PreviousShiftId { get; set; } = string.Empty;

	[Required]
	public string NewShiftId { get; set; } = string.Empty;

	[Required]
	public DateOnly EffectiveFrom { get; set; }

	[MaxLength(500)]
	public string? Remarks { get; set; }

	[ForeignKey(nameof(EmployeeId))]
	public HrmEmployeeMaster? Employee { get; set; }

	[ForeignKey(nameof(PreviousShiftId))]
	public MstShift? PreviousShift { get; set; }

	[ForeignKey(nameof(NewShiftId))]
	public MstShift? NewShift { get; set; }
}
