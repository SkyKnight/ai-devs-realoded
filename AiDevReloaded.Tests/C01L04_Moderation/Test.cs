using AiDevReloaded.Core.AiTasks;
using AiDevReloaded.Core.OpenAI.Moderation;

namespace AiDevReloaded.Tests._04_Moderation;

public sealed class Test
{
    [Fact]
    public async Task Should_Pass_Moderation_Task()
    {
        var taskSession = new TaskSession<TaskResponse, int[]>(Secrets.AiDevTaskApiKey, "moderation");
        var taskContent = await taskSession.GetTaskContent();
        var moderationService = new ModerationService(Secrets.OpenAiApiKey);
        var answer = new List<int>();
        foreach (var input in taskContent.Input)
        {
            var isRequireModeration = await moderationService.IsRequireModeration(input);
            answer.Add(isRequireModeration ? 1 : 0);
        }
        await taskSession.SendAnswer(answer.ToArray());
    }
}
