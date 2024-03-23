using AiDevReloaded.Core.AiTasks;

namespace AiDevReloaded.Tests._05_liar;

public sealed class TaskResponse : ApiResponse
{
    public string Answer { get; set; } = string.Empty;
}
