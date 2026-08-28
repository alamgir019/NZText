using NZ.Leave.Application.Interfaces.Repositories;
using NZ.Leave.Application.LeaveEncashmentRequests.Enums;

namespace NZ.Leave.Application.LeaveEncashmentRequests.Commands.DeleteLeaveEncashmentRequest
{
    public class DeleteLeaveEncashmentRequestCommandHandler
    {
        private readonly ILeaveEncashmentRequestRepository _repository;

        public DeleteLeaveEncashmentRequestCommandHandler(ILeaveEncashmentRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<DeleteLeaveEncashmentRequestResult> Handle(DeleteLeaveEncashmentRequestCommand command, CancellationToken cancellationToken = default)
        {
            var existing = await _repository.GetByIdAsync(command.RequestId, cancellationToken);
            if (existing == null)
            {
                return new DeleteLeaveEncashmentRequestResult
                {
                    Success = false,
                    ErrorCode = "VAL-NOTFOUND",
                    Message = "Leave request not found."
                };
            }

            // VAL-012: Only DRAFT requests can be deleted
            if (!string.Equals(existing.Status, LeaveEncashmentRequestStatus.Draft, StringComparison.OrdinalIgnoreCase))
            {
                return new DeleteLeaveEncashmentRequestResult
                {
                    Success = false,
                    ErrorCode = "VAL-012",
                    Message = "Only DRAFT requests can be deleted."
                };
            }

            await _repository.DeleteAsync(command.RequestId, cancellationToken);

            return new DeleteLeaveEncashmentRequestResult
            {
                Success = true,
                Message = "Leave request deleted successfully."
            };
        }
    }
}
