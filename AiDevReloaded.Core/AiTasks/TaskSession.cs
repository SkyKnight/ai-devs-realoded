namespace AiDevReloaded.Core.AiTasks;

public sealed class TaskSession<TContent, TAnswer>
    where TContent : ApiResponse, new()
{
    private readonly TaskService _taskService;
    private readonly string _apiKey;
    private readonly string _taskName;
    private string _token = string.Empty;

    public TaskSession(string apiKey, string taskName)
    {
        _apiKey = apiKey;
        _taskName = taskName;
        _taskService = new TaskService();
    }

    public async Task<TContent> GetTaskContent(CancellationToken cancellationToken = default)
    {
        _token = await _taskService.GetToken(_apiKey, _taskName, cancellationToken);
        return await _taskService.GetTaskContent<TContent>(_token, cancellationToken);
    }

    public async Task<TContent> PostTaskContent(IReadOnlyDictionary<string, string> content, CancellationToken cancellationToken = default)
    {
        return await _taskService.PostTaskContent<TContent>(_token, content, cancellationToken);
    }

    public async Task SendAnswer(TAnswer answer, CancellationToken cancellationToken = default)
    {
        await _taskService.SendAnswer(_token, answer, cancellationToken);
    }
}
