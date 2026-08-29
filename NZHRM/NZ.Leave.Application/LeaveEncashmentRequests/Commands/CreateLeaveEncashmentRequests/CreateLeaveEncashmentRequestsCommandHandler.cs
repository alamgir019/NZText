using NZ.Leave.Application.Interfaces.Repositories;
using NZ.Leave.Application.LeaveEncashmentRequests.Dto;
using NZ.Leave.Application.LeaveEncashmentRequests.Enums;
using NZ.Shared.Contracts.Leave;

namespace NZ.Leave.Application.LeaveEncashmentRequests.Commands.CreateLeaveEncashmentRequests
{
    public class CreateLeaveEncashmentRequestsCommandHandler
    {
        private readonly ILeaveEncashmentRequestRepository _repository;
        private readonly ILeaveBalanceQuery _leaveBalanceQuery;

        public CreateLeaveEncashmentRequestsCommandHandler(
            ILeaveEncashmentRequestRepository repository,
            ILeaveBalanceQuery leaveBalanceQuery)
        {
            _repository = repository;
            _leaveBalanceQuery = leaveBalanceQuery;
        }

        public async Task<CreateLeaveEncashmentRequestsResult> Handle(CreateLeaveEncashmentRequestsCommand command, CancellationToken cancellationToken = default)
        {
            if (command.Requests == null || command.Requests.Count == 0)
            {
                return new CreateLeaveEncashmentRequestsResult
                {
                    Success = false,
                    ErrorCode = "VAL-014",
                    Message = "No leave requests available for forwarding."
                };
            }

            var validationError = ValidateRequests(command.Requests);
            if (validationError != null)
                return validationError;

            decimal totalDays = 0;
            var createdIds = new List<string>();

            foreach (var req in command.Requests)
            {
                // VAL-BALANCE: Validate leave balance for the selected leave type
                var balance = await _leaveBalanceQuery.GetBalanceAsync(req.EmployeeId, req.LeaveType, req.EncashDate.Year, cancellationToken);
                if (balance == null)
                {
                    return new CreateLeaveEncashmentRequestsResult
                    {
                        Success = false,
                        ErrorCode = "VAL-BALANCE",
                        Message = $"Employee {req.EmployeeId} is absent in Leave Balance."
                    };
                }

                if (balance.Balance < req.EncashDays)
                {
                    return new CreateLeaveEncashmentRequestsResult
                    {
                        Success = false,
                        ErrorCode = "VAL-BALANCE",
                        Message = $"Insufficient leave balance for employee {req.EmployeeId}."
                    };
                }

                var dto = new LeaveEncashmentRequestDto
                {
                    EmployeeId = req.EmployeeId,
                    EmployeeName = req.EmployeeName,
                    LeaveType = req.LeaveType,
                    EncashDate = req.EncashDate,
                    EncashDays = req.EncashDays,
                    Reason = req.Reason,
                    Status = LeaveEncashmentRequestStatus.Forwarded,
                    FromDate = req.FromDate,
                    ToDate = req.ToDate,
                    CreatedBy = command.CreatedBy,
                    CreatedDate = DateTime.UtcNow,
                    ForwardedBy = req.ForwardedBy,
                    ForwardedDate = req.ForwardedDate.HasValue ? req.ForwardedDate.Value.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc) : null
                };

                var id = await _repository.CreateAsync(dto, cancellationToken);
                createdIds.Add(id);
                totalDays += req.EncashDays;
            }

            return new CreateLeaveEncashmentRequestsResult
            {
                Success = true,
                Message = "Leave requests created successfully.",
                TotalEmployees = command.Requests.Count,
                TotalDays = totalDays
            };
        }

        private static CreateLeaveEncashmentRequestsResult? ValidateRequests(List<CreateLeaveEncashmentRequestItem> requests)
        {
            foreach (var req in requests)
            {
                if (string.IsNullOrWhiteSpace(req.EmployeeId))
                    return Error("VAL-001", "Employee ID is required.");

                if (string.IsNullOrWhiteSpace(req.EmployeeName))
                    return Error("VAL-002", "Employee Name is required.");

                if (string.IsNullOrWhiteSpace(req.LeaveType))
                    return Error("VAL-003", "Leave Type is required.");

                if (req.EncashDate == default)
                    return Error("VAL-005", "EncashDate is required.");

                if (string.IsNullOrWhiteSpace(req.Reason))
                    return Error("VAL-007", "Reason is required.");

                if (req.Reason.Length > 250)
                    return Error("VAL-008", "Reason length cannot exceed 250 characters.");

                if (req.EncashDays <= 0)
                    return Error("VAL-009", "Total Days must be greater than 0.");
            }

            return null;
        }

        private static CreateLeaveEncashmentRequestsResult Error(string code, string message) =>
            new CreateLeaveEncashmentRequestsResult { Success = false, ErrorCode = code, Message = message };
    }
}
