using NZ.Leave.Application.Interfaces.Repositories;
using NZ.Leave.Application.LeaveEncashmentRequests.Enums;

namespace NZ.Leave.Application.LeaveEncashmentRequests.Commands.UpdateLeaveEncashmentRequest
{
    public class UpdateLeaveEncashmentRequestCommandHandler
    {
        private readonly ILeaveEncashmentRequestRepository _repository;

        public UpdateLeaveEncashmentRequestCommandHandler(ILeaveEncashmentRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<UpdateLeaveEncashmentRequestResult> Handle(UpdateLeaveEncashmentRequestCommand command, CancellationToken cancellationToken = default)
        {
            var existing = await _repository.GetByIdAsync(command.RequestId, cancellationToken);
            if (existing == null)
                return Error("VAL-NOTFOUND", "Leave request not found.");

            // VAL-011: Only DRAFT requests can be updated
            if (!string.Equals(existing.Status, LeaveEncashmentRequestStatus.Draft, StringComparison.OrdinalIgnoreCase))
                return Error("VAL-011", "Only DRAFT requests can be updated.");

            if (string.IsNullOrWhiteSpace(command.EmployeeId))
                return Error("VAL-001", "Employee ID is required.");

            if (string.IsNullOrWhiteSpace(command.EmployeeName))
                return Error("VAL-002", "Employee Name is required.");

            if (string.IsNullOrWhiteSpace(command.LeaveType))
                return Error("VAL-003", "Leave Type is required.");

            if (command.EncashDate == default)
                return Error("VAL-005", "EncashDate is required.");

            if (string.IsNullOrWhiteSpace(command.Reason))
                return Error("VAL-007", "Reason is required.");

            if (command.Reason.Length > 250)
                return Error("VAL-008", "Reason length cannot exceed 250 characters.");

            if (command.EncashDays <= 0)
                return Error("VAL-009", "Total Days must be greater than 0.");

            existing.LeaveType = command.LeaveType;
            existing.EmployeeId = command.EmployeeId;
            existing.EmployeeName = command.EmployeeName;
            existing.EncashDate = command.EncashDate;
            existing.EncashDays = command.EncashDays;
            existing.Reason = command.Reason;
            existing.ModifiedBy = command.ModifiedBy;
            existing.ModifiedDate = DateTime.UtcNow;
            existing.ForwardedBy = command.ForwardedBy ?? existing.ForwardedBy;
            existing.ForwardedDate = command.ForwardedDate.HasValue
                ? command.ForwardedDate.Value.ToDateTime(TimeOnly.MinValue)
                : existing.ForwardedDate;

            await _repository.UpdateAsync(existing, cancellationToken);

            return new UpdateLeaveEncashmentRequestResult
            {
                Success = true,
                Message = "Leave request updated successfully."
            };
        }

        private static UpdateLeaveEncashmentRequestResult Error(string code, string message) =>
            new UpdateLeaveEncashmentRequestResult { Success = false, ErrorCode = code, Message = message };
    }
}
