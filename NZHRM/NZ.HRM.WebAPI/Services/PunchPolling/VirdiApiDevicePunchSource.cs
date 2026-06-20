using System.Globalization;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace NZ.HRM.WebAPI.Services.PunchPolling;

public class VirdiApiDevicePunchSource : IDevicePunchSource
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    private readonly HttpClient _httpClient;
    private readonly PunchPollingOptions _options;
    private readonly ILogger<VirdiApiDevicePunchSource> _logger;

    public VirdiApiDevicePunchSource(
        HttpClient httpClient,
        IOptions<PunchPollingOptions> options,
        ILogger<VirdiApiDevicePunchSource> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<List<DevicePunchRecord>> PullNewPunchesAsync(string unit, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken)
    {
        ValidateOptions(unit);

        var requestUri = BuildRequestUri(unit, fromDate, toDate);
        using var response = await _httpClient.GetAsync(requestUri, cancellationToken);
        response.EnsureSuccessStatusCode();

        await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        var payload = await JsonSerializer.DeserializeAsync<VirdiApiResponse>(stream, JsonOptions, cancellationToken);

        if (payload == null)
        {
            _logger.LogWarning("Virdi API returned an empty response for unit {Unit}", unit);
            return new List<DevicePunchRecord>();
        }

        if (!string.Equals(payload.Status, "success", StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogWarning("Virdi API returned status {Status} for unit {Unit}", payload.Status, unit);
            return new List<DevicePunchRecord>();
        }

        return payload.Data
            .Select(x => ParseRecord(x, unit))
            .Where(x => x != null)
            .Select(x => x!)
            .OrderBy(x => x.PunchDate)
            .ThenBy(x => x.PunchTime)
            .ToList();
    }

    private void ValidateOptions(string unit)
    {
        if (string.IsNullOrWhiteSpace(_options.BaseUrl))
            throw new InvalidOperationException("PunchPolling:BaseUrl is required.");
        if (string.IsNullOrWhiteSpace(_options.ApiKey))
            throw new InvalidOperationException("PunchPolling:ApiKey is required.");
        if (string.IsNullOrWhiteSpace(unit))
            throw new InvalidOperationException("PunchPolling:Units must contain at least one valid unit.");
    }

    private string BuildRequestUri(string unit, DateTime fromDate, DateTime toDate)
    {
        var builder = new UriBuilder(_options.BaseUrl);
        var query = new List<string>
        {
            $"apikey={Uri.EscapeDataString(_options.ApiKey)}",
            $"pUnit={Uri.EscapeDataString(unit)}",
            $"pDateFrom={Uri.EscapeDataString(fromDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))}",
            $"pDateTo={Uri.EscapeDataString(toDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))}"
        };

        builder.Query = string.Join("&", query);
        return builder.Uri.ToString();
    }

    private DevicePunchRecord? ParseRecord(VirdiApiPunchRecord value, string unit)
    {
        if (!DateTime.TryParseExact(
                value.PunchTime,
                "yyyy-MM-dd HH:mm:ss.fff",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var punchDateTime))
        {
            _logger.LogWarning("Unable to parse Virdi punch timestamp {Timestamp}", value.PunchTime);
            return null;
        }

        if (string.IsNullOrWhiteSpace(value.PunchCardId))
        {
            _logger.LogWarning("Skipping Virdi punch with empty PunchCardID at {Timestamp}", value.PunchTime);
            return null;
        }

        return new DevicePunchRecord
        {
            EmployeeCode = value.PunchCardId,
            PunchDate = DateOnly.FromDateTime(punchDateTime),
            PunchTime = TimeOnly.FromDateTime(punchDateTime),
            DeviceId = string.IsNullOrWhiteSpace(_options.DeviceName) ? unit : _options.DeviceName,
            DeviceLocation = string.IsNullOrWhiteSpace(_options.DeviceLocation) ? unit : _options.DeviceLocation,
            VerificationMode = _options.VerificationMode,
            PunchSource = $"{_options.PunchSource}:{unit}"
        };
    }
}
