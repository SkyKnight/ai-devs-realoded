using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel;

namespace AiDevReloaded.Core.Services;

public sealed class GenericOpenAiService
{
    private readonly string _apiKey;

    public GenericOpenAiService(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task<string> Ask(string prompt)
    {
        var builder = Kernel.CreateBuilder();
        builder.AddOpenAIChatCompletion("gpt-3.5-turbo", _apiKey);
        var kernel = builder.Build();


        var storyWriter = kernel.CreateFunctionFromPrompt(prompt, executionSettings: new OpenAIPromptExecutionSettings() { MaxTokens = 1024 });
        var result = await kernel.InvokeAsync(storyWriter);
        return result.GetValue<string>();
    }
}
