using AiDevReloaded.Core.AiTasks;

namespace AiDevReloaded.Tests._04_Blogger;

public sealed class TaskResponse : ApiResponse
{
    public string[] Blog { get; set; } = Array.Empty<string>();
}
