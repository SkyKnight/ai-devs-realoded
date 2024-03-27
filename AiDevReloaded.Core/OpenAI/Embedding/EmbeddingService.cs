using Flurl.Http;

namespace AiDevReloaded.Core.OpenAI.Embedding;

public sealed class EmbeddingService
{
    private const string API_URL = "https://api.openai.com/v1/embeddings";

    private readonly string _apiKey;

    public EmbeddingService(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task<Response> GetEmbeddingInfo(string input, CancellationToken cancellationToken = default)
    {
        return await API_URL.WithOAuthBearerToken(_apiKey)
            .PostJsonAsync(new { input, model = "text-embedding-ada-002" }, cancellationToken: cancellationToken)
            .ReceiveJson<Response>();
    }
}
