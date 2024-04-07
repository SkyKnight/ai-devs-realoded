using AiDevReloaded.Core.AiTasks;
using AiDevReloaded.Core.Services;

namespace AiDevReloaded.Tests.C03L02_scraper;

public sealed class Test
{
    [Fact]
    public async Task Should_Pass_Scraper_Task()
    {
        var taskSession = new TaskSession<TaskResponse, string>(Secrets.AiDevTaskApiKey, "scraper");
        var taskContent = await taskSession.GetTaskContent();
        var scraper = new ScrappingService();
        var content = await scraper.Scrap(taskContent.Input);
        var openAiService = new GenericOpenAiService(Secrets.OpenAiApiKey);
        var prompt = $@"{taskContent.Message}
###
{content}
###
{taskContent.Question}";
        var answer = await openAiService.Ask(prompt);

        await taskSession.SendAnswer(answer);
    }
}

