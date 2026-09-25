using NZ.Leave.Application.Interfaces.Repositories;
using NZ.Leave.Application.LeaveEncashmentRequests.Enums;

namespace NZ.Leave.Application.LeaveEncashmentRequests.Commands.ProcessLeaveEncashmentAction
{
    public class ProcessLeaveEncashmentActionCommandHandler
    {
        private readonly ILeaveEncashmentRequestRepository _repository;

        public ProcessLeaveEncashmentActionCommandHandler(ILeaveEncashmentRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProcessLeaveEncashmentActionResult> Handle(ProcessLeaveEncashmentActionCommand command, CancellationToken cancellationToken = default)
        {
            if (command == null || string.IsNullOrWhiteSpace(command.RequestId))
                return Error("INVALID_REQUEST", "Request ID is mandatory.");

            var action = command.Action?.Trim().ToUpperInvariant();
            //if (string.IsNullOrWhiteSpace(action) || !LeaveEncashmentRequestAction.All.Contains(action))
            //    return Error("INVALID_REQUEST", "Action must be either FORWARD or REJECT.");

            var existing = await _repository.GetByIdAsync(command.RequestId, cancellationToken);
            if (existing == null)
                return Error("REQUEST_NOT_FOUND", "Leave encashment request not found.");

            if (string.Equals(existing.Status, LeaveEncashmentRequestStatus.Rejected, StringComparison.OrdinalIgnoreCase))
                return Error("INVALID_REQUEST", "Rejected requests cannot be processed again.");

            if (string.Equals(existing.Status, LeaveEncashmentRequestStatus.Approved, StringComparison.OrdinalIgnoreCase))
                return Error("INVALID_REQUEST", "Approved requests cannot be processed again.");

            //if (!string.Equals(existing.Status, LeaveEncashmentRequestStatus.Pending, StringComparison.OrdinalIgnoreCase))
            //    return Error("INVALID_REQUEST", "Request must be in PENDING status.");

            var processedBy = string.IsNullOrWhiteSpace(command.ProcessedBy)
                ? "SYSTEM"
                : command.ProcessedBy!;

            await _repository.ProcessActionAsync(
                command.RequestId,
                action,
                command.Remarks,
                processedBy,
                cancellationToken);

            //var targetStatus = action == LeaveEncashmentRequestAction.Forward
            //    ? LeaveEncashmentRequestStatus.Forwarded
            //    : LeaveEncashmentRequestStatus.Rejected;

            return new ProcessLeaveEncashmentActionResult
            {
                Success = true,
                RequestId = command.RequestId,
                Action = action,
                Status = action,
                Message = action == LeaveEncashmentRequestAction.Forward
                    ? "Request forwarded successfully."
                    : "Request rejected successfully."
            };
        }

        private static ProcessLeaveEncashmentActionResult Error(string code, string message) =>
            new ProcessLeaveEncashmentActionResult
            {
                Success = false,
                ErrorCode = code,
                Message = message
            };
    }
}