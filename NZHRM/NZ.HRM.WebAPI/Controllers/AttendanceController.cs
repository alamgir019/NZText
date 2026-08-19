using Microsoft.AspNetCore.Mvc;
using NZ.Attendance.Infrastructure.Contracts;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/attendance")]
public class AttendanceController : ControllerBase
{
    private readonly IAttendanceDashboardQuery _attendanceDashboardQuery;

    public AttendanceController(IAttendanceDashboardQuery attendanceDashboardQuery)
    {
        _attendanceDashboardQuery = attendanceDashboardQuery;
    }

    [HttpGet("summary")]
    [ProducesResponseType(typeof(ShiftAttendanceSummaryResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAttendanceSummary(
        [FromQuery] string? shiftId = null,
        [FromQuery] DateOnly? attendanceDate = null,
        [FromQuery] bool includeDepartments = true,
        [FromQuery] string? departmentId = null)
    {
        var result = await _attendanceDashboardQuery.GetShiftSummaryAsync(
            shiftId, attendanceDate, includeDepartments, departmentId, cancellationToken: default);

        if (result == null)
        {
            if (!string.IsNullOrWhiteSpace(shiftId))
            {
                return NotFound(new { code = "SHIFT_NOT_FOUND", message = "Specified shift does not exist." });
            }

            return NotFound(new { code = "ATTENDANCE_NOT_AVAILABLE", message = "Attendance data not available for selected date and shift." });
        }

        return Ok(result);
    }
}
