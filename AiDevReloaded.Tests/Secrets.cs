namespace AiDevReloaded.Tests;

public static class Secrets
{
    public static string AiDevTaskApiKey => Environment.GetEnvironmentVariable("AI_DEVS_API_KEY") ?? throw new Exception("AI_DEVS_API_KEY is not set");
}
