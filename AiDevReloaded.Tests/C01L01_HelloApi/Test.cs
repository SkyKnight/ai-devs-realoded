using AiDevReloaded.Core.AiTasks;

namespace AiDevReloaded.Tests._01_HelloApi;

public sealed class Test
{
    [Fact]
    public async Task Should_Pass_HelloApi_Task()
    {
        var taskSession = new TaskSession<TaskResponse, string>(Secrets.AiDevTaskApiKey, "helloapi");
        var taskContent = await taskSession.GetTaskContent();
        var answer = taskContent.Cookie;
        await taskSession.SendAnswer(answer);
    }
}