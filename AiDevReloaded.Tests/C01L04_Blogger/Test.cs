using AiDevReloaded.Core.AiTasks;
using AiDevReloaded.Core.Services;

namespace AiDevReloaded.Tests._04_Blogger;

public sealed class Test
{
    [Fact]
    public async Task Should_Pass_Blogger_Task()
    {
        var taskSession = new TaskSession<TaskResponse, string[]>(Secrets.AiDevTaskApiKey, "blogger");
        var taskContent = await taskSession.GetTaskContent();
        var storyWriter = new PizzaStoryWriter(Secrets.OpenAiApiKey);
        var answer = await storyWriter.WriteStory(taskContent.Blog);

        await taskSession.SendAnswer(answer.ToArray());
    }
}
