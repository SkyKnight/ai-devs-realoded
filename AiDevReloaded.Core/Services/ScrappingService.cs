using Flurl.Http;
using Polly;

namespace AiDevReloaded.Core.Services;

public sealed class ScrappingService
{
    private readonly IAsyncPolicy<string> _policy;

    public ScrappingService()
    {
        _policy = Policy<string>
            .Handle<Exception>()
            .WaitAndRetryForeverAsync(attempt => TimeSpan.FromSeconds(1));
    }

    public async Task<string> Scrap(string url)
    {
        return await _policy.ExecuteAsync(async () =>
        {
            return await url.WithHeader("User-Agent", "Mozilla/5.0 (platform; rv:geckoversion) Gecko/geckotrail Firefox/firefoxversion").GetStringAsync();
        });
    }
}
