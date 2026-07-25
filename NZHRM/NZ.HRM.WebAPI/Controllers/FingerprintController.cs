using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.Services;

namespace NZ.HRM.WebAPI.Controllers;

/// <summary>
/// API endpoints for fingerprint device integration
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class FingerprintController : ControllerBase
{
    private readonly IFingerprintDeviceService _fingerprintService;
    private readonly ILogger<FingerprintController> _logger;

    public FingerprintController(
        IFingerprintDeviceService fingerprintService,
        ILogger<FingerprintController> logger)
    {
        _fingerprintService = fingerprintService;
        _logger = logger;
    }

    /// <summary>
    /// Gets the employee code from fingerprint device using device ID
    /// </summary>
    /// <param name="deviceId">Device ID (e.g., "device2")</param>
    /// <returns>Employee code</returns>
    /// <response code="200">Returns the employee code</response>
    /// <response code="400">If device ID is missing or invalid</response>
    /// <response code="404">If employee code not found for the device</response>
    /// <response code="500">If there's an error communicating with fingerprint device</response>
    [HttpGet("verify-employee-code")]
    [ProducesResponseType(typeof(FingerprintDeviceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> VerifyEmployeeCode([FromQuery] string deviceId)
    {
        if (string.IsNullOrWhiteSpace(deviceId))
        {
            _logger.LogWarning("VerifyEmployeeCode called without deviceId parameter");
            return BadRequest(new { message = "deviceId parameter is required" });
        }

        try
        {
            var employeeCode = await _fingerprintService.VerifyEmployeeCodeAsync(deviceId);

            if (string.IsNullOrWhiteSpace(employeeCode))
            {
                _logger.LogWarning($"No employee code found for device: {deviceId}");
                return NotFound(new { message = $"No employee code found for device: {deviceId}" });
            }

            _logger.LogInformation($"Successfully retrieved employee code: {employeeCode} for device: {deviceId}");

            return Ok(new FingerprintDeviceResponse
            {
                Success = true,
                EmployeeCode = employeeCode,
                DeviceId = deviceId,
                Timestamp = DateTime.UtcNow,
                Message = "Employee code retrieved successfully"
            });
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning($"Invalid argument: {ex.Message}");
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError($"Invalid operation: {ex.Message}");
            return StatusCode(500, new
            {
                message = "Error communicating with fingerprint device",
                details = ex.Message
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected error: {ex.Message}");
            return StatusCode(500, new
            {
                message = "An unexpected error occurred",
                details = ex.Message
            });
        }
    }
}

/// <summary>
/// Response model for fingerprint device API
/// </summary>
public class FingerprintDeviceResponse
{
    /// <summary>
    /// Indicates whether the request was successful
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// The employee code retrieved from the device
    /// </summary>
    public string? EmployeeCode { get; set; }

    /// <summary>
    /// The device ID that was queried
    /// </summary>
    public string? DeviceId { get; set; }

    /// <summary>
    /// Timestamp of the response (UTC)
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Response message
    /// </summary>
    public string? Message { get; set; }
}

/// <summary>
/// Detailed response model for fingerprint device API with performance metrics
/// </summary>
public class FingerprintDeviceDetailedResponse
{
    /// <summary>
    /// Indicates whether the request was successful
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// The employee code retrieved from the device
    /// </summary>
    public string? EmployeeCode { get; set; }

    /// <summary>
    /// The device ID that was queried
    /// </summary>
    public string? DeviceId { get; set; }

    /// <summary>
    /// When the request started
    /// </summary>
    public DateTime RequestStartTime { get; set; }

    /// <summary>
    /// When the request completed
    /// </summary>
    public DateTime RequestEndTime { get; set; }

    /// <summary>
    /// Total elapsed time in milliseconds
    /// </summary>
    public long ElapsedMilliseconds { get; set; }

    /// <summary>
    /// Response message
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Error details if the request failed
    /// </summary>
    public string? ErrorDetails { get; set; }
}
