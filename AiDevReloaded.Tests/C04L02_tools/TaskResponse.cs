using AiDevReloaded.Core.AiTasks;

namespace AiDevReloaded.Tests.C04L02_tools;

public sealed class TaskResponse : ApiResponse
{
    public string Question { get; set; } = string.Empty;
}
