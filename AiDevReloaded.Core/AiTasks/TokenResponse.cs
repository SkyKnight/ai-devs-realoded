using System.Text.Json.Serialization;

namespace AiDevReloaded.Core.AiTasks;

public sealed class TokenResponse : ApiResponse
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;
}
