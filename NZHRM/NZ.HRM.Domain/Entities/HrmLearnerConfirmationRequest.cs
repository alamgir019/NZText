using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;
using NZ.HRM.Domain.Enums;

namespace NZ.HRM.Domain.Entities
{
    /// <summary>
    /// A request forwarding a learner for permanency (confirmation) approval.
    /// </summary>
    [Table("learner_confirmation_request", Schema = "hrm")]
    public class HrmLearnerConfirmationRequest : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty; // FK to employee_master.Id

        public DateOnly DateOfJoining { get; set; }
        public int ProbationPeriodMonths { get; set; }
        public DateOnly ProbationCompletedOn { get; set; }

        public decimal CurrentGrossSalary { get; set; }
        public decimal StandardGrossSalary { get; set; }
        public decimal AdjustmentAmount { get; set; }

        public string Status { get; set; } = LearnerConfirmationStatus.Forwarded.ToString();

        public string ForwardedBy { get; set; } = string.Empty;
        public DateTime ForwardedOn { get; set; }

        public string? ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string? Remarks { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }

        public bool IsPending => string.Equals(
            Status, LearnerConfirmationStatus.Forwarded.ToString(), StringComparison.OrdinalIgnoreCase);

        public static HrmLearnerConfirmationRequest Forward(
            string employeeId,
            DateOnly dateOfJoining,
            int probationPeriodMonths,
            DateOnly probationCompletedOn,
            decimal currentGrossSalary,
            decimal standardGrossSalary,
            decimal adjustmentAmount,
            string forwardedBy,
            string? remarks)
            => new()
            {
                EmployeeId = employeeId,
                DateOfJoining = dateOfJoining,
                ProbationPeriodMonths = probationPeriodMonths,
                ProbationCompletedOn = probationCompletedOn,
                CurrentGrossSalary = currentGrossSalary,
                StandardGrossSalary = standardGrossSalary,
                AdjustmentAmount = adjustmentAmount,
                Status = LearnerConfirmationStatus.Forwarded.ToString(),
                ForwardedBy = forwardedBy,
                ForwardedOn = DateTime.UtcNow,
                Remarks = remarks
            };

        public void Approve(string approvedBy, string? remarks)
        {
            EnsurePending();
            Status = LearnerConfirmationStatus.Approved.ToString();
            ApprovedBy = approvedBy;
            ApprovalDate = DateTime.UtcNow;
            Remarks = remarks ?? Remarks;
        }

        public void Reject(string rejectedBy, string? remarks)
        {
            EnsurePending();
            Status = LearnerConfirmationStatus.Rejected.ToString();
            ApprovedBy = rejectedBy;
            ApprovalDate = DateTime.UtcNow;
            Remarks = remarks ?? Remarks;
        }

        private void EnsurePending()
        {
            if (!IsPending)
                throw new InvalidOperationException(
                    $"Learner confirmation request {Id} is already {Status}.");
        }
    }
}
