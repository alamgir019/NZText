using NZ.Leave.Application.Interfaces.Repositories;
using NZ.Leave.Application.LeaveRequests.Enums;

namespace NZ.Leave.Application.LeaveRequests.Commands.DeleteLeaveRequest
{
    public class DeleteLeaveRequestCommandHandler
    {
        private readonly ILeaveRequestRepository _repository;

        public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<DeleteLeaveRequestResult> Handle(DeleteLeaveRequestCommand command, CancellationToken cancellationToken = default)
        {
            var existing = await _repository.GetByIdAsync(command.RequestId, cancellationToken);
            if (existing == null)
            {
                return new DeleteLeaveRequestResult
                {
                    Success = false,
                    ErrorCode = "VAL-NOTFOUND",
                    Message = "Leave request not found."
                };
            }

            // VAL-012: Only DRAFT requests can be deleted
            if (!string.Equals(existing.Status, RequestStatus.Draft, StringComparison.OrdinalIgnoreCase))
            {
                return new DeleteLeaveRequestResult
                {
                    Success = false,
                    ErrorCode = "VAL-012",
                    Message = "Only DRAFT requests can be deleted."
                };
            }

            await _repository.DeleteAsync(command.RequestId, cancellationToken);

            return new DeleteLeaveRequestResult
            {
                Success = true,
                Message = "Leave request deleted successfully."
            };
        }
    }
}
