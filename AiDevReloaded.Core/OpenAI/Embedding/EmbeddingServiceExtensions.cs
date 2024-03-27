namespace AiDevReloaded.Core.OpenAI.Embedding;

public static class EmbeddingServiceExtensions
{
    public static async Task<double[]> GetEmbedding(this EmbeddingService service, string input, CancellationToken cancellationToken = default)
    {
        var response = await service.GetEmbeddingInfo(input, cancellationToken);
        return response.Data.First().Embedding.ToArray();
    }
}
