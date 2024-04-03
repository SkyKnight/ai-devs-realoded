using AiDevReloaded.Core.AiTasks;
using AiDevReloaded.Core.OpenAI.Transcription;
using Flurl.Http;
using Newtonsoft.Json.Linq;

namespace AiDevReloaded.Tests.C02L05_functions;

public sealed class Test
{
    [Fact]
    public async Task Should_Pass_Functions_Task()
    {
        var taskSession = new TaskSession<DefaultResponse, JObject>(Secrets.AiDevTaskApiKey, "functions");
        var taskContent = await taskSession.GetTaskContent();
        var function = JObject.Parse(await File.ReadAllTextAsync("C02L05_functions/functions.json"));

        await taskSession.SendAnswer(function);
    }
}
