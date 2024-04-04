using AiDevReloaded.Core.AiTasks;
using Newtonsoft.Json.Linq;

namespace AiDevReloaded.Tests.C03L01_rodo;

public sealed class Test
{
    [Fact]
    public async Task Should_Pass_Rodo_Task()
    {
        var taskSession = new TaskSession<DefaultResponse, JObject>(Secrets.AiDevTaskApiKey, "rodo");
        var taskContent = await taskSession.GetTaskContent();
        var function = JObject.Parse(await File.ReadAllTextAsync("C02L05_functions/functions.json"));

        await taskSession.SendAnswer(function);
    }
}