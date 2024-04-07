using AiDevReloaded.Core.AiTasks;
using AiDevReloaded.Core.Services;

namespace AiDevReloaded.Tests.C03L03_whoami;

public sealed class Test
{
    [Fact]
    public async Task Should_Pass_WhoAmI_Task()
    {
        var taskSession = new TaskSession<TaskResponse, string>(Secrets.AiDevTaskApiKey, "whoami");
        //var taskContent = await taskSession.GetTaskContent();
        var peopleGuessingService = new PeopleGuessingService(Secrets.OpenAiApiKey);

        var answer = await peopleGuessingService.Guess(async () => (await taskSession.GetTaskContent()).Hint);

        await taskSession.SendAnswer(answer);
    }
}
