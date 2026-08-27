using NZ.Leave.Application.Interfaces.Repositories;
using NZ.Leave.Application.LeaveRequests.Dto;
using NZ.Leave.Application.LeaveRequests.Enums;
using NZ.Shared.Contracts.Leave;

namespace NZ.Leave.Application.LeaveRequests.Commands.CreateLeaveRequests
{
    public class CreateLeaveRequestsCommandHandler
    {
        private readonly ILeaveRequestRepository _repository;
        private readonly ILeaveBalanceQuery _leaveBalanceQuery;

        public CreateLeaveRequestsCommandHandler(
            ILeaveRequestRepository repository,
            ILeaveBalanceQuery leaveBalanceQuery)
        {
            _repository = repository;
            _leaveBalanceQuery = leaveBalanceQuery;
        }

        public async Task<CreateLeaveRequestsResult> Handle(CreateLeaveRequestsCommand command, CancellationToken cancellationToken = default)
        {
            if (command.Requests == null || command.Requests.Count == 0)
            {
                return new CreateLeaveRequestsResult
                {
                    Success = false,
                    ErrorCode = "VAL-014",
                    Message = "No leave requests available for forwarding."
                };
            }

            // VAL-010: Reject duplicate employees within the same request batch
            var seen = new HashSet<string>();
            foreach (var req in command.Requests)
            {
                if (!seen.Add(req.EmployeeId))
                {
                    return new CreateLeaveRequestsResult
                    {
                        Success = false,
                        ErrorCode = "VAL-010",
                        Message = "Duplicate employee found in request list."
                    };
                }
            }

            var validationError = ValidateRequests(command.Requests);
            if (validationError != null)
                return validationError;

            decimal totalDays = 0;
            var createdIds = new List<string>();

            foreach (var req in command.Requests)
            {
                var days = CalculateTotalDays(req.FromDate, req.ToDate);

                // VAL-006: Validate leave balance for the selected leave type
                var balance = await _leaveBalanceQuery.GetBalanceAsync(req.EmployeeId, req.LeaveType, req.FromDate.Year, cancellationToken);
                if (balance != null && balance.Balance < days)
                {
                    return new CreateLeaveRequestsResult
                    {
                        Success = false,
                        ErrorCode = "VAL-BALANCE",
                        Message = $"Insufficient leave balance for employee {req.EmployeeId}."
                    };
                }

                if (balance == null)
                {
                    return new CreateLeaveRequestsResult
                    {
                        Success = false,
                        ErrorCode = "VAL-BALANCE",
                        Message = $"Employee {req.EmployeeId} is absent in Leave Balance."
                    };
                }

                var dto = new LeaveRequestDto
                {
                    EmployeeId = req.EmployeeId,
                    EmployeeName = req.EmployeeName,
                    LeaveType = req.LeaveType,
                    FromDate = req.FromDate,
                    ToDate = req.ToDate,
                    TotalDays = days,
                    Reason = req.Reason,
                    Status = RequestStatus.Forwarded,
                    CreatedBy = command.CreatedBy,
                    CreatedDate = DateTime.UtcNow,
                    ForwardedBy = req.ForwardedBy,
                    ForwardedDate = req.ForwardedDate.HasValue ? req.ForwardedDate.Value.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc) : null
                };

                var id = await _repository.CreateAsync(dto, cancellationToken);
                createdIds.Add(id);
                totalDays += days;
            }

            return new CreateLeaveRequestsResult
            {
                Success = true,
                Message = "Leave requests created successfully.",
                TotalEmployees = command.Requests.Count,
                TotalDays = totalDays
            };
        }

        private static CreateLeaveRequestsResult? ValidateRequests(List<CreateLeaveRequestItem> requests)
        {
            foreach (var req in requests)
            {
                if (string.IsNullOrWhiteSpace(req.EmployeeId))
                    return Error("VAL-001", "Employee ID is required.");

                if (string.IsNullOrWhiteSpace(req.EmployeeName))
                    return Error("VAL-002", "Employee Name is required.");

                if (string.IsNullOrWhiteSpace(req.LeaveType))
                    return Error("VAL-003", "Leave Type is required.");

                if (req.FromDate == default)
                    return Error("VAL-004", "From Date is required.");

                if (req.ToDate == default)
                    return Error("VAL-005", "To Date is required.");

                if (req.ToDate < req.FromDate)
                    return Error("VAL-006", "To Date must be greater than or equal to From Date.");

                if (string.IsNullOrWhiteSpace(req.Reason))
                    return Error("VAL-007", "Reason is required.");

                if (req.Reason.Length > 250)
                    return Error("VAL-008", "Reason length cannot exceed 250 characters.");

                var totalDays = CalculateTotalDays(req.FromDate, req.ToDate);
                if (totalDays <= 0)
                    return Error("VAL-009", "Total Days must be greater than 0.");
            }

            return null;
        }

        private static CreateLeaveRequestsResult Error(string code, string message) =>
            new CreateLeaveRequestsResult { Success = false, ErrorCode = code, Message = message };

        internal static decimal CalculateTotalDays(DateOnly fromDate, DateOnly toDate) =>
            (toDate.DayNumber - fromDate.DayNumber) + 1;
    }
}
