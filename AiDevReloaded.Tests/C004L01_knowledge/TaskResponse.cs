using AiDevReloaded.Core.AiTasks;

namespace AiDevReloaded.Tests.C004L01_knowledge;

public sealed class TaskResponse : ApiResponse
{
    public string Question { get; set; } = string.Empty;
}