using AiDevReloaded.Core.AiTasks;
using AiDevReloaded.Core.Services;

namespace AiDevReloaded.Tests.C004L01_knowledge;

public sealed class Test
{
    [Fact]
    public async Task Should_Pass_Knowledge_Task()
    {
        var taskSession = new TaskSession<TaskResponse, string>(Secrets.AiDevTaskApiKey, "knowledge");
        var taskContent = await taskSession.GetTaskContent();

        var knowledgeService = new KnowledgeService(Secrets.OpenAiApiKey);
        var answer = await knowledgeService.Ask(taskContent.Question);
        
        await taskSession.SendAnswer(answer);
    }
}