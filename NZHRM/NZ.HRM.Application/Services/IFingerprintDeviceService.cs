namespace NZ.HRM.Application.Services;

/// <summary>
/// Interface for fingerprint device operations
/// </summary>
public interface IFingerprintDeviceService
{
    /// <summary>
    /// Verifies the employee code from fingerprint device using device ID
    /// </summary>
    /// <param name="deviceId">Device ID (e.g., "device2")</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Employee code as string</returns>
    Task<string?> VerifyEmployeeCodeAsync(string deviceId, CancellationToken cancellationToken = default);
}
