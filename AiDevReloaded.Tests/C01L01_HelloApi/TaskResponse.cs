using AiDevReloaded.Core.AiTasks;
using System.Text.Json.Serialization;

namespace AiDevReloaded.Tests._01_HelloApi;

public sealed class TaskResponse : ApiResponse
{
    [JsonPropertyName("cookie")]
    public string Cookie { get; set; } = string.Empty;
}
