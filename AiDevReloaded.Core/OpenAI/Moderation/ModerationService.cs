using Flurl.Http;

namespace AiDevReloaded.Core.OpenAI.Moderation;

public sealed class ModerationService
{
    private const string API_URL = "https://api.openai.com/v1/moderations";

    private readonly string _apiKey;

    public ModerationService(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task<Response> GetModerationInfo(string input, CancellationToken cancellationToken = default)
    {
        return await API_URL.WithOAuthBearerToken(_apiKey)
            .PostJsonAsync(new { input }, cancellationToken: cancellationToken)
            .ReceiveJson<Response>();
    }
}
