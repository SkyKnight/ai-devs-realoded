using AiDevReloaded.Core.AiTasks;
using AiDevReloaded.Core.RenderForm;
using AiDevReloaded.Core.Services;

namespace AiDevReloaded.Tests.C05L01_meme;

public sealed class Test
{
    [Fact]
    public async Task Should_Pass_Meme_Task()
    {
        var taskSession = new TaskSession<TaskResponse, string>(Secrets.AiDevTaskApiKey, "meme");
        var taskContent = await taskSession.GetTaskContent();

        var renderFormService = new RenderFormService(Secrets.RenderFormApiKey);
        var answer = await renderFormService.GenerateMeme(taskContent.Text, taskContent.Image);

        await taskSession.SendAnswer(answer);
    }
}
