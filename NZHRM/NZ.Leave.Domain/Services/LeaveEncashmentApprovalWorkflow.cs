using NZ.Leave.Domain.Entities;

namespace NZ.Leave.Domain.Services
{
    public class LeaveEncashmentApprovalWorkflow
    {
        private const string PendingStatus = "PENDING";
        private const string ForwardedStatus = "FORWARDED";
        private const string RejectedStatus = "REJECTED";
        private const string ForwardAction = "FORWARD";
        private const string RejectAction = "REJECT";

        public LevLeaveEncashmentHistory ApplyAction(
            LevLeaveEncashment entity,
            string action,
            string processedBy,
            string? remarks,
            int workflowStepNo)
        {
            ArgumentNullException.ThrowIfNull(entity);

            if (string.IsNullOrWhiteSpace(processedBy))
                throw new ArgumentException("Processed by is required.", nameof(processedBy));

            if (!string.Equals(entity.Status, PendingStatus, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Only PENDING requests can be processed.");

            var normalizedAction = action.Trim().ToUpperInvariant();
            var nextStatus = normalizedAction switch
            {
                ForwardAction => ForwardedStatus,
                RejectAction => RejectedStatus,
                _ => throw new InvalidOperationException("Unsupported leave encashment action.")
            };

            entity.Status = nextStatus;
            entity.UpdatedBy = processedBy;
            entity.UpdatedOn = DateTime.UtcNow;

            return new LevLeaveEncashmentHistory
            {
                EncashmentId = entity.Id,
                WorkflowStepNo = workflowStepNo,
                ApproverId = processedBy,
                ActionTaken = normalizedAction,
                Remarks = remarks ?? string.Empty,
                CreatedBy = processedBy,
                UpdatedBy = processedBy
            };
    }
}
    }