using System.ComponentModel.DataAnnotations.Schema;

using NZ.HRM.Domain.Enums;

namespace NZ.HRM.Domain.Entities
{
    /// <summary>
    /// A request forwarding a learner for permanency (confirmation) approval.
    /// </summary>
    [Table("learner_confirmation_request", Schema = "hrm")]
    public class HrmLearnerConfirmationRequest : BaseEntityWithSortOrder
    {
        private const int ConfirmationStepNo = 1;
        private const string ConfirmationStepName = "Learner Confirmation";

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

        public ICollection<HrmLearnerConfirmationApprovalHistory> ApprovalHistories { get; set; }
            = new List<HrmLearnerConfirmationApprovalHistory>();

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
                Remarks = string.IsNullOrWhiteSpace(remarks) ? null : remarks.Trim()
            };

        public HrmLearnerConfirmationApprovalHistory Approve(string approvedBy, string? remarks)
        {
            EnsurePending();
            var fromStatus = Status;
            Status = LearnerConfirmationStatus.Approved.ToString();
            ApprovedBy = approvedBy;
            ApprovalDate = DateTime.UtcNow;
            Remarks = string.IsNullOrWhiteSpace(remarks) ? Remarks : remarks.Trim();

            return RecordTransition(
                LearnerConfirmationStatus.Approved.ToString(),
                fromStatus,
                LearnerConfirmationStatus.Approved.ToString(),
                approvedBy,
                remarks);
        }

        public HrmLearnerConfirmationApprovalHistory Reject(string rejectedBy, string? remarks)
        {
            EnsurePending();
            var fromStatus = Status;
            Status = LearnerConfirmationStatus.Rejected.ToString();
            ApprovedBy = rejectedBy;
            ApprovalDate = DateTime.UtcNow;
            Remarks = string.IsNullOrWhiteSpace(remarks) ? Remarks : remarks.Trim();

            return RecordTransition(
                LearnerConfirmationStatus.Rejected.ToString(),
                fromStatus,
                LearnerConfirmationStatus.Rejected.ToString(),
                rejectedBy,
                remarks);
        }
        public HrmLearnerConfirmationApprovalHistory ForwardToMovementSection(string forwardedBy, string? remarks)
        {
            var fromStatus = Status;
            Status = LearnerConfirmationStatus.ForwardedMovementSection.ToString();
            ApprovedBy = forwardedBy;
            ApprovalDate = DateTime.UtcNow;
            Remarks = string.IsNullOrWhiteSpace(remarks) ? Remarks : remarks.Trim();

            return RecordTransition(
                LearnerConfirmationStatus.ForwardedMovementSection.ToString(),
                fromStatus,
                LearnerConfirmationStatus.ForwardedMovementSection.ToString(),
                forwardedBy,
                remarks);
        }

        public HrmLearnerConfirmationApprovalHistory ForwardToMovementHR(string forwardedBy, string? remarks)
        {
            var fromStatus = Status;
            Status = LearnerConfirmationStatus.ForwardedHR.ToString();
            ApprovedBy = forwardedBy;
            ApprovalDate = DateTime.UtcNow;
            Remarks = string.IsNullOrWhiteSpace(remarks) ? Remarks : remarks.Trim();

            return RecordTransition(
                LearnerConfirmationStatus.ForwardedHR.ToString(),
                fromStatus,
                LearnerConfirmationStatus.ForwardedHR.ToString(),
                forwardedBy,
                remarks);
        }

        public HrmLearnerConfirmationApprovalHistory ForwardToCEO(string forwardedBy, string? remarks)
        {
            var fromStatus = Status;
            Status = LearnerConfirmationStatus.ForwardedCEO.ToString();
            ApprovedBy = forwardedBy;
            ApprovalDate = DateTime.UtcNow;
            Remarks = string.IsNullOrWhiteSpace(remarks) ? Remarks : remarks.Trim();

            return RecordTransition(
                LearnerConfirmationStatus.ForwardedCEO.ToString(),
                fromStatus,
                LearnerConfirmationStatus.ForwardedCEO.ToString(),
                forwardedBy,
                remarks);
        }

        private HrmLearnerConfirmationApprovalHistory RecordTransition(
            string action,
            string fromStatus,
            string toStatus,
            string actionBy,
            string? remarks)
        {
            var actionOn = DateTime.UtcNow;

            var history = new HrmLearnerConfirmationApprovalHistory
            {
                LearnerConfirmationRequestId = Id,
                StepNo = ConfirmationStepNo,
                StepName = ConfirmationStepName,
                Action = action,
                FromStatus = fromStatus,
                ToStatus = toStatus,
                ActionBy = actionBy,
                ActionOn = actionOn,
                Remarks = remarks,
                CreatedBy = actionBy,
                UpdatedBy = actionBy
            };

            UpdatedBy = actionBy;
            UpdatedOn = actionOn;
            ApprovalHistories.Add(history);
            return history;
        }

        private void EnsurePending()
        {
            if (!IsPending)
                throw new InvalidOperationException(
                    $"Learner confirmation request {Id} is already {Status}.");
        }
    }
}
