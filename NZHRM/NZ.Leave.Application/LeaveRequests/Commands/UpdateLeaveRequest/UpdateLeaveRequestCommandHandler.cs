using NZ.Leave.Application.Interfaces.Repositories;
using NZ.Leave.Application.LeaveRequests.Enums;

namespace NZ.Leave.Application.LeaveRequests.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommandHandler
    {
        private readonly ILeaveRequestRepository _repository;

        public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<UpdateLeaveRequestResult> Handle(UpdateLeaveRequestCommand command, CancellationToken cancellationToken = default)
        {
            var existing = await _repository.GetByIdAsync(command.RequestId, cancellationToken);
            if (existing == null)
                return Error("VAL-NOTFOUND", "Leave request not found.");

            // VAL-011: Only DRAFT requests can be updated
            if (!string.Equals(existing.Status, RequestStatus.Draft, StringComparison.OrdinalIgnoreCase))
                return Error("VAL-011", "Only DRAFT requests can be updated.");

            if (string.IsNullOrWhiteSpace(command.LeaveType))
                return Error("VAL-003", "Leave Type is required.");

            if (command.FromDate == default)
                return Error("VAL-004", "From Date is required.");

            if (command.ToDate == default)
                return Error("VAL-005", "To Date is required.");

            if (command.ToDate < command.FromDate)
                return Error("VAL-006", "To Date must be greater than or equal to From Date.");

            if (string.IsNullOrWhiteSpace(command.Reason))
                return Error("VAL-007", "Reason is required.");

            if (command.Reason.Length > 250)
                return Error("VAL-008", "Reason length cannot exceed 250 characters.");

            var totalDays = CreateLeaveRequests.CreateLeaveRequestsCommandHandler.CalculateTotalDays(command.FromDate, command.ToDate);
            if (totalDays <= 0)
                return Error("VAL-009", "Total Days must be greater than 0.");

            existing.LeaveType = command.LeaveType;
            existing.FromDate = command.FromDate;
            existing.ToDate = command.ToDate;
            existing.TotalDays = totalDays;
            existing.Reason = command.Reason;
            existing.ApprovedBy = command.ApprovedBy;
            existing.ApprovedDate = DateTime.UtcNow;
            existing.ApproveStatus = command.Approved ? "APPROVED" : "REJECTED";

            await _repository.UpdateAsync(existing, cancellationToken);

            return new UpdateLeaveRequestResult
            {
                Success = true,
                Message = "Leave request updated successfully."
            };
        }

        private static UpdateLeaveRequestResult Error(string code, string message) =>
            new UpdateLeaveRequestResult { Success = false, ErrorCode = code, Message = message };
    }
}
