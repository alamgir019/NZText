// Add the namespace where OvertimeRequestDto and OvertimeEmployeeDto are defined
// For example: using NZ.HRM.Application.DTOs;

using NZ.Attendance.Application.OvertimeRequests.Commands.ApproveOvertimeRequest;
using NZ.Attendance.Application.OvertimeRequests.Dto;

namespace NZ.Attendance.Application.Interfaces.Repositories
{
    public interface IOvertimeRequestRepository
    {
        Task<string> CreateAsync(OvertimeRequestDto dto);
        Task AddEmployeeAsync(string overtimeRequestId, OvertimeEmployeeDto dto);
        Task<OvertimeRequestDto?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        /// <summary>
        /// Returns paged results and total count.
        /// </summary>
        Task<(List<OvertimeRequestDto> Items, int Total)> GetAllAsync(int pageNumber = 1, int pageSize = 20,
            string? shiftId = null, string? departmentId = null, DateTime? from = null, DateTime? to = null, string? status = null,
            CancellationToken cancellationToken = default);

        Task ApproveAsync(List<ApproveOvertimeRequestCommand> commands, CancellationToken cancellationToken = default);
        // Update approval for a specific item (employee-level)
        Task UpdateEmployeeApprovalAsync(string itemId, string approvedBy, bool approved = true);
        // Get employees assigned to a shift (via shift roster) with employment/designation/department info
        Task<List<EmployeeByShiftDto>> GetEmployeesByShiftAndDepartmentAsync(string shiftId, string departmentId, CancellationToken cancellationToken = default);
        // Additional methods (Update, RemoveEmployee, Submit) can be added later
    }
}
