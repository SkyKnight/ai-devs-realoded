using AiDevReloaded.Core.AiTasks;

namespace AiDevReloaded.Tests.C05L01_meme;

public sealed class TaskResponse : ApiResponse
{
    public string Image { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;
}
