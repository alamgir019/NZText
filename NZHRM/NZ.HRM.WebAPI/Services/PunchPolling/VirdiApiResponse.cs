using System.Text.Json.Serialization;

namespace NZ.HRM.WebAPI.Services.PunchPolling;

public class VirdiApiPunchRecord
{
    [JsonPropertyName("PunchCardID")]
    public string PunchCardId { get; set; } = string.Empty;

    [JsonPropertyName("PunchTime")]
    public string PunchTime { get; set; } = string.Empty;
}

public class VirdiApiResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("row_count")]
    public int RowCount { get; set; }

    [JsonPropertyName("data")]
    public List<VirdiApiPunchRecord> Data { get; set; } = new();
}
