namespace NZ.HRM.WebAPI.Configuration;

/// <summary>
/// Configuration for fingerprint device API integration
/// </summary>
public class FingerprintDeviceConfiguration
{
    /// <summary>
    /// Gets or sets the base URL for the fingerprint device API
    /// Example: http://175.29.147.115:8000/virdi/finger-print.php
    /// </summary>
    public string BaseUrl { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the HTTP request timeout in seconds
    /// </summary>
    public int TimeoutSeconds { get; set; } = 30;

    /// <summary>
    /// Gets or sets the number of retry attempts for failed requests
    /// </summary>
    public int RetryAttempts { get; set; } = 3;

    /// <summary>
    /// Gets or sets the delay between retry attempts in milliseconds
    /// </summary>
    public int RetryDelayMs { get; set; } = 500;
}
