namespace AiDevReloaded.Tests;

public static class Secrets
{
    public static string AiDevTaskApiKey => Environment.GetEnvironmentVariable("AI_DEVS_API_KEY") ?? throw new Exception("AI_DEVS_API_KEY is not set");

    public static string OpenAiApiKey => Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? throw new Exception("OPENAI_API_KEY is not set");

    public static string RenderFormApiKey => Environment.GetEnvironmentVariable("RENDERFORM_API_KEY") ?? throw new Exception("RENDERFORM_API_KEY is not set");
}
