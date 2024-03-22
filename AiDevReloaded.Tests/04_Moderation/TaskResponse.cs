using AiDevReloaded.Core.AiTasks;

namespace AiDevReloaded.Tests._04_Moderation;

public sealed class TaskResponse : ApiResponse
{
    public string[] Input { get; set; } = Array.Empty<string>();
}
