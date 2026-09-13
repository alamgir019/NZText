using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NZ.HRM.Domain.Entities;

[Table("employee_transfer", Schema = "hrm")]
public class HrmEmployeeTransfer : BaseEntity
{
	[Required]
	public string EmployeeId { get; set; } = string.Empty;

	[Required]
	public string PreviousDepartmentId { get; set; } = string.Empty;

	[Required]
	public string PreviousSectionId { get; set; } = string.Empty;

	[Required]
	public string NewDepartmentId { get; set; } = string.Empty;

	[Required]
	public string NewSectionId { get; set; } = string.Empty;

	[Required]
	public DateOnly EffectiveFrom { get; set; }

	[MaxLength(500)]
	public string? Remarks { get; set; }

	[ForeignKey(nameof(EmployeeId))]
	public HrmEmployeeMaster? Employee { get; set; }

	[ForeignKey(nameof(PreviousDepartmentId))]
	public MstDepartment? PreviousDepartment { get; set; }

	[ForeignKey(nameof(PreviousSectionId))]
	public MstSection? PreviousSection { get; set; }

	[ForeignKey(nameof(NewDepartmentId))]
	public MstDepartment? NewDepartment { get; set; }

	[ForeignKey(nameof(NewSectionId))]
	public MstSection? NewSection { get; set; }
}
