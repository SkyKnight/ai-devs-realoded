using AiDevReloaded.Core.AiTasks;
using AiDevReloaded.Core.OpenAI.Moderation;
using AiDevReloaded.Core.Services;

namespace AiDevReloaded.Tests._05_liar;

public sealed class Test
{
    [Fact]
    public async Task Should_Pass_Liar_Task()
    {
        var taskSession = new TaskSession<TaskResponse, string>(Secrets.AiDevTaskApiKey, "liar");
        var taskContent = await taskSession.GetTaskContent();
        var question = "What is capital of Poland?";
        var questionAnswer = (await taskSession.PostTaskContent(new Dictionary<string, string> { { "question", question } })).Answer;
        var truthService = new TruthService(Secrets.OpenAiApiKey);
        var isTrue = await truthService.IsTrue(question, questionAnswer);

        await taskSession.SendAnswer(isTrue ? "YES" : "NO");
    }
}