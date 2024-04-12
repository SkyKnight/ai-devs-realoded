using AiDevReloaded.Core.AiTasks;
using AiDevReloaded.Core.Services;
using Newtonsoft.Json.Linq;

namespace AiDevReloaded.Tests.C04L03_gnome;

public sealed class Test
{
    [Fact]
    public async Task Should_Pass_Gnome_Task()
    {
        var taskSession = new TaskSession<TaskResponse, string>(Secrets.AiDevTaskApiKey, "gnome");
        var taskContent = await taskSession.GetTaskContent();

        var gnomeService = new GnomeService(Secrets.OpenAiApiKey);
        var answer = await gnomeService.GetHatColor(taskContent.Url);

        await taskSession.SendAnswer(answer);
    }
}