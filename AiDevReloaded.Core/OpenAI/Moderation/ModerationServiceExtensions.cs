namespace AiDevReloaded.Core.OpenAI.Moderation;

public static class ModerationServiceExtensions
{
    public static async Task<bool> IsRequireModeration(this ModerationService service, string input, CancellationToken cancellationToken = default)
    {
        var response = await service.GetModerationInfo(input, cancellationToken);
        return response.Results.Any(x => x.Flagged);
    }
}
