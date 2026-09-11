using Microsoft.AspNetCore.Mvc;
using NZ.Attendance.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;

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

    // Returns punch level summary for a unit for the current or previous shift
    [HttpGet("punch-summary")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPunchSummary([
        FromQuery] string? unitId,
        [FromQuery] bool isPrevious = false)
    {
        if (string.IsNullOrWhiteSpace(unitId))
            return BadRequest(new { code = "UNIT_REQUIRED", message = "UnitId is required." });
        var summary = await _attendanceDashboardQuery.GetPunchSummaryAsync(unitId!, isPrevious, cancellationToken: default);
        if (summary == null) return NotFound(new { code = "SHIFT_NOT_FOUND", message = "Unable to resolve shift or no attendance available." });

        return Ok(summary);
    }

}
