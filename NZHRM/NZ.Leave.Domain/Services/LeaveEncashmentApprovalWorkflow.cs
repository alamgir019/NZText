using NZ.Leave.Domain.Constants;
using NZ.Leave.Domain.Entities;

namespace NZ.Leave.Domain.Services
{
    public class LeaveEncashmentApprovalWorkflow
    {

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

            var nextStatus = ResolveStatusFromAction(action);
            var normalizedAction = action.Trim().ToUpperInvariant();

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

        public static string ResolveStatusFromAction(string action)
        {
            if (string.IsNullOrWhiteSpace(action))
                throw new InvalidOperationException("Unsupported leave encashment action.");

            var normalizedAction = action.Trim().ToUpperInvariant();

            if (new[]
                {
                    LeaveEncashmentRequestAction.ForwardedToSectionInCharge,
                    LeaveEncashmentRequestAction.ForwardedToAttendanceCell,
                    LeaveEncashmentRequestAction.ForwardedToHR,
                    LeaveEncashmentRequestAction.ForwardedToCEO
                }.Contains(normalizedAction, StringComparer.OrdinalIgnoreCase))
            {
                return LeaveEncashmentRequestStatus.Forwarded;
            }

            return normalizedAction switch
            {
                LeaveEncashmentRequestAction.RejectedFromHR => LeaveEncashmentRequestStatus.Rejected,
                LeaveEncashmentRequestAction.RejectedFromCEO => LeaveEncashmentRequestStatus.Rejected,
                LeaveEncashmentRequestStatus.Approved => LeaveEncashmentRequestStatus.Approved,
                LeaveEncashmentRequestStatus.Cancelled => LeaveEncashmentRequestStatus.Cancelled,
                LeaveEncashmentRequestStatus.Pending => LeaveEncashmentRequestStatus.Pending,
                _ => throw new InvalidOperationException("Unsupported leave encashment action.")
            };
        }
    }
}
