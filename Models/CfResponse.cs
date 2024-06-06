using System.Text.Json.Serialization;

namespace turnstile.Models;

public class CfResponse
{
    public bool Success { get; set; } = false;

    [JsonPropertyName("challenge_ts")]
    public DateTime? ChallengeTs { get; set; }
    public string Hostname { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;

    [JsonPropertyName("cdata")]
    public string CData { get; set; } = string.Empty;
}