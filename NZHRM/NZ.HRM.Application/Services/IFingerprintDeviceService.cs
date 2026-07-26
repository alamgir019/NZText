namespace NZ.HRM.Application.Services;

/// <summary>
/// Interface for fingerprint device operations
/// </summary>
public interface IFingerprintDeviceService
{
    /// <summary>
    /// Verifies the employee code from fingerprint device using device ID and unit
    /// </summary>
    /// <param name="deviceId">Device ID (e.g., "device2")</param>
    /// <param name="unit">Unit identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Employee code as string</returns>
    Task<string?> VerifyEmployeeCodeAsync(string deviceId, string unit, CancellationToken cancellationToken = default);
}
