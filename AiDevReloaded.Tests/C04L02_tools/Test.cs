using AiDevReloaded.Core.AiTasks;
using AiDevReloaded.Core.Services;
using Newtonsoft.Json.Linq;

namespace AiDevReloaded.Tests.C04L02_tools;

public sealed class Test
{
    [Fact]
    public async Task Should_Pass_Tools_Task()
    {
        var taskSession = new TaskSession<TaskResponse, JObject>(Secrets.AiDevTaskApiKey, "tools");
        var taskContent = await taskSession.GetTaskContent();

        var toolsService = new ToolsService(Secrets.OpenAiApiKey);
        var answer = await toolsService.Ask(taskContent.Question);

        await taskSession.SendAnswer(JObject.Parse(answer));
    }
}