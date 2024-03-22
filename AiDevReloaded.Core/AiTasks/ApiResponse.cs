using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;

namespace AiDevReloaded.Core.AiTasks;

public abstract class ApiResponse
{
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("msg")]
    public string Message { get; set; } = string.Empty;
}
