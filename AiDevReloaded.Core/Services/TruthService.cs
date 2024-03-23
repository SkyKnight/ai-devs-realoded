namespace AiDevReloaded.Core.Services;

public sealed class TruthService
{
    private readonly string _apiKey;

    public TruthService(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task<bool> IsTrue(string question, string answer)
    {
        return false;
    }
}
