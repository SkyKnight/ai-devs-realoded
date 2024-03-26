using AiDevReloaded.Core.AiTasks;
using AiDevReloaded.Core.Services;

namespace AiDevReloaded.Tests.C02L02_inprompt;

public sealed class Test
{
    [Fact]
    public async Task Should_Pass_InPrompt_Task()
    {
        var taskSession = new TaskSession<TaskResponse, string>(Secrets.AiDevTaskApiKey, "inprompt");
        var taskContent = await taskSession.GetTaskContent();
        var knowledge = new PeopleKnowledgeService(taskContent.Input);
        var service = new PeopleService(knowledge, Secrets.OpenAiApiKey);
        var question = taskContent.Question;
        var personName = await service.GetPersonName(question);
        var answer = await service.Ask(question, personName);

        await taskSession.SendAnswer(answer);
    }
}
