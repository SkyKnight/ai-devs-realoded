using AiDevReloaded.Core.AiTasks;
using AiDevReloaded.Core.OpenAI.Embedding;
using AiDevReloaded.Core.Services;

namespace AiDevReloaded.Tests.C02L03_embedding;

public sealed class Test
{
    [Fact]
    public async Task Should_Pass_Embedding_Task()
    {
        var taskSession = new TaskSession<DefaultResponse, double[]>(Secrets.AiDevTaskApiKey, "embedding");
        var taskContent = await taskSession.GetTaskContent();
        var sentence = "Hawaiian pizza";
        var embeddingService = new EmbeddingService(Secrets.OpenAiApiKey);
        var answer = await embeddingService.GetEmbedding(sentence);


        await taskSession.SendAnswer(answer);
    }
}
