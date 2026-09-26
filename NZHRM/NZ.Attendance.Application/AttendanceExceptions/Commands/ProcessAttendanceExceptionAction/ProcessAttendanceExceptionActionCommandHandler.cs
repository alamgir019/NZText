using NZ.Attendance.Application.Interfaces.Repositories;

namespace NZ.Attendance.Application.AttendanceExceptions.Commands.ProcessAttendanceExceptionAction
{
    public class ProcessAttendanceExceptionActionCommandHandler
    {
        private const string ForwardAction = "FORWARD-TO-IT";
        private const string RejectAction = "REJECTED";

        private readonly IAttendanceExceptionRepository _repository;

        public ProcessAttendanceExceptionActionCommandHandler(IAttendanceExceptionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProcessAttendanceExceptionActionResult> Handle(
            ProcessAttendanceExceptionActionCommand command,
            CancellationToken cancellationToken = default)
        {
            if (command == null || string.IsNullOrWhiteSpace(command.RequestId))
                return Error("INVALID_REQUEST", "Request ID is mandatory.");

            var action = command.Action?.Trim().ToUpperInvariant();
            if (action != ForwardAction && action != RejectAction)
                return Error("INVALID_REQUEST", "Action must be either FORWARD or REJECT.");

            var existing = await _repository.GetByIdAsync(command.RequestId, cancellationToken);
            if (existing == null)
                return Error("INVALID_REQUEST", "Attendance exception request cannot be processed.");

            if (string.Equals(existing.Status, RejectAction, StringComparison.OrdinalIgnoreCase))
                return Error("INVALID_REQUEST", "Already rejected requests cannot be processed.");

            var processedBy = string.IsNullOrWhiteSpace(command.ProcessedBy)
                ? "SYSTEM"
                : command.ProcessedBy;

            if (action == ForwardAction)
            {
                await _repository.ForwardToITAsync(command.RequestId, processedBy, command.Remarks, cancellationToken);
            }
            else
            {
                await _repository.RejectAsync(command.RequestId, processedBy, command.Remarks, cancellationToken);
            }

            return new ProcessAttendanceExceptionActionResult
            {
                Success = true,
                RequestId = command.RequestId,
                Action = action,
                Status = action,
                Message = action == ForwardAction
                    ? "Attendance exception request forwarded to IT successfully."
                    : "Attendance exception request rejected successfully."
            };
        }

        private static ProcessAttendanceExceptionActionResult Error(string errorCode, string message) =>
            new()
            {
                Success = false,
                ErrorCode = errorCode,
                Message = message
            };
    }
}