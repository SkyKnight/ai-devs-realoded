using Flurl.Http;

namespace AiDevReloaded.Core.OpenAI.Transcription;

public sealed class TranscriptionService
{
    private const string API_URL = "https://api.openai.com/v1/audio/transcriptions";

    private readonly string _apiKey;

    public TranscriptionService(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task<string> GetText(Stream stream, CancellationToken cancellationToken = default)
    {
        var response = await API_URL.WithOAuthBearerToken(_apiKey)
            .PostMultipartAsync(mp => mp.AddFile("file", stream, "speech.mp3")
                .AddString("model", "whisper-1")
                .AddString("response_format", "text"), cancellationToken: cancellationToken)
            .ReceiveString();

        return response;
    }
}
