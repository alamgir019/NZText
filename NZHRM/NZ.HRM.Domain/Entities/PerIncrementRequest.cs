using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace NZ.HRM.Domain.Entities
{
	[Table("increment_request", Schema = "payroll")]
	public class PerIncrementRequest: BaseEntity
	{
		public string PayIncHistId { get; set; } = string.Empty;
		public string? ApprovedBy { get; set; }
		public DateTime? ApprovalDate { get; set; }

		[ForeignKey(nameof(PayIncHistId))]
		public PayIncrementHistory? PayIncrementHistory { get; set; }

	}
}
