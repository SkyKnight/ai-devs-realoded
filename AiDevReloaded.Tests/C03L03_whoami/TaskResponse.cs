using AiDevReloaded.Core.AiTasks;

namespace AiDevReloaded.Tests.C03L03_whoami;

public sealed class TaskResponse : ApiResponse
{
    public string Hint { get; set; } = string.Empty;
}
