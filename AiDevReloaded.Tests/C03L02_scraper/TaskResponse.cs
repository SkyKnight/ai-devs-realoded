using AiDevReloaded.Core.AiTasks;

namespace AiDevReloaded.Tests.C03L02_scraper;

public sealed class TaskResponse : ApiResponse
{
    public string Input { get; set; } = string.Empty;

    public string Question { get; set; } = string.Empty;
}
