using AiDevReloaded.Core.AiTasks;

namespace AiDevReloaded.Tests.C02L02_inprompt;

public sealed class TaskResponse : ApiResponse
{
    public string[] Input { get; set; } = Array.Empty<string>();

    public string Question { get; set; } = string.Empty;
}
