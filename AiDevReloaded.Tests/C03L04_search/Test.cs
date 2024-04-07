using AiDevReloaded.Core.AiTasks;
using AiDevReloaded.Core.Services;

namespace AiDevReloaded.Tests.C03L04_search;

public sealed class Test
{
    [Fact]
    public async Task Should_Pass_Search_Task()
    {
        var taskSession = new TaskSession<TaskResponse, string>(Secrets.AiDevTaskApiKey, "search");
        var taskContent = await taskSession.GetTaskContent();
        var archiveService = new ArchiveService(Secrets.OpenAiApiKey, "C03L04_search/archiwum_aidevs.json");
        
        var answer = await archiveService.FindUrl(taskContent.Question);

        await taskSession.SendAnswer(answer);
    }
}
