using AiDevReloaded.Core.AiTasks;
using AiDevReloaded.Core.OpenAI.Embedding;
using AiDevReloaded.Core.OpenAI.Transcription;
using Flurl.Http;

namespace AiDevReloaded.Tests.C02L04_whisper;

public sealed class Test
{
    [Fact]
    public async Task Should_Pass_Whisper_Task()
    {
        var taskSession = new TaskSession<DefaultResponse, string>(Secrets.AiDevTaskApiKey, "whisper");
        var taskContent = await taskSession.GetTaskContent();
        var transcriptionService = new TranscriptionService(Secrets.OpenAiApiKey);
        var voiceMessage = await "https://tasks.aidevs.pl/data/mateusz.mp3".GetStreamAsync();
        var answer = await transcriptionService.GetText(voiceMessage);


        await taskSession.SendAnswer(answer);
    }
}
