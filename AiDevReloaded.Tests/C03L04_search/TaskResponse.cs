using AiDevReloaded.Core.AiTasks;

namespace AiDevReloaded.Tests.C03L04_search;

public sealed class TaskResponse : ApiResponse
{
    public string Question { get; set; } = string.Empty;
}
