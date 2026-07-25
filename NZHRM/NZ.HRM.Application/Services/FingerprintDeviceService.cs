using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NZ.HRM.Domain.Configuration;

namespace NZ.HRM.Application.Services;

/// <summary>
/// Service for interacting with fingerprint device API
/// </summary>
public class FingerprintDeviceService : IFingerprintDeviceService
{
    private readonly FingerprintDeviceConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly ILogger<FingerprintDeviceService> _logger;

    public FingerprintDeviceService(
        FingerprintDeviceConfiguration configuration,
        HttpClient httpClient,
        ILogger<FingerprintDeviceService> logger)
    {
        _configuration = configuration;
        _httpClient = httpClient;
        _logger = logger;
    }

    /// <summary>
    /// Verifies the employee code from fingerprint device using device ID
    /// </summary>
    public async Task<string?> VerifyEmployeeCodeAsync(string deviceId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(deviceId))
        {
            _logger.LogWarning("Device ID is required");
            throw new ArgumentException("Device ID is required", nameof(deviceId));
        }

        if (string.IsNullOrWhiteSpace(_configuration.BaseUrl))
        {
            _logger.LogError("Fingerprint device BaseUrl is not configured");
            throw new InvalidOperationException("Fingerprint device BaseUrl is not configured");
        }

        var url = BuildUrl(deviceId);
        _logger.LogInformation($"Fetching employee code for device: {deviceId} from URL: {url}");

        try
        {
            var result = await ExecuteWithRetryAsync(url, cancellationToken);

            if (string.IsNullOrWhiteSpace(result))
            {
                _logger.LogWarning($"Empty response from fingerprint device for device ID: {deviceId}");
                return null;
            }

            var employeeCode = result.Trim();
            _logger.LogInformation($"Successfully fetched employee code: {employeeCode} for device: {deviceId}");

            return employeeCode;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"HTTP request error while fetching employee code for device {deviceId}: {ex.Message}");
            throw new InvalidOperationException($"Failed to fetch employee code from fingerprint device: {ex.Message}", ex);
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogError($"Request timeout while fetching employee code for device {deviceId}");
            throw new InvalidOperationException($"Request timeout while fetching employee code: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected error while fetching employee code for device {deviceId}: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Builds the complete URL with device ID parameter
    /// </summary>
    private string BuildUrl(string deviceId)
    {
        // URL encode the device ID to handle special characters
        var encodedDeviceId = Uri.EscapeDataString(deviceId);

        // Check if BaseUrl already contains query parameters
        var separator = _configuration.BaseUrl.Contains('?') ? "&" : "?";

        return $"{_configuration.BaseUrl}{separator}deviceId={encodedDeviceId}";
    }

    /// <summary>
    /// Executes HTTP request with retry logic
    /// </summary>
    private async Task<string> ExecuteWithRetryAsync(string url, CancellationToken cancellationToken)
    {
        int attempts = 0;
        int maxAttempts = Math.Max(1, _configuration.RetryAttempts);

        while (attempts < maxAttempts)
        {
            try
            {
                attempts++;
                _logger.LogDebug($"Attempt {attempts}/{maxAttempts} to fetch from {url}");

                using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(_configuration.TimeoutSeconds)))
                using (var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cts.Token))
                {
                    var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseContentRead, linkedCts.Token);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync(cancellationToken);
                        return content;
                    }

                    _logger.LogWarning($"Attempt {attempts}: Fingerprint device returned status code {response.StatusCode}");

                    // Don't retry on client errors (4xx)
                    if ((int)response.StatusCode >= 400 && (int)response.StatusCode < 500)
                    {
                        throw new HttpRequestException($"Client error: {response.StatusCode} - {response.ReasonPhrase}");
                    }

                    // Retry on server errors (5xx) or network issues
                    if (attempts < maxAttempts)
                    {
                        var delayMs = _configuration.RetryDelayMs * attempts; // Exponential backoff
                        _logger.LogDebug($"Retrying after {delayMs}ms...");
                        await Task.Delay(delayMs, cancellationToken);
                    }
                }
            }
            catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
            {
                // This is a timeout, log and potentially retry
                _logger.LogWarning($"Attempt {attempts}: Request timeout");

                if (attempts < maxAttempts)
                {
                    var delayMs = _configuration.RetryDelayMs * attempts;
                    _logger.LogDebug($"Retrying after {delayMs}ms...");
                    await Task.Delay(delayMs, cancellationToken);
                }
                else
                {
                    throw new InvalidOperationException($"Request timed out after {attempts} attempts");
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning($"Attempt {attempts}: HTTP error - {ex.Message}");

                if (attempts < maxAttempts)
                {
                    var delayMs = _configuration.RetryDelayMs * attempts;
                    _logger.LogDebug($"Retrying after {delayMs}ms...");
                    await Task.Delay(delayMs, cancellationToken);
                }
                else
                {
                    throw;
                }
            }
        }

        throw new InvalidOperationException($"Failed to fetch employee code after {maxAttempts} attempts");
    }
}
