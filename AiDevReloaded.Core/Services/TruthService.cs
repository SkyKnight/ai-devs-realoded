using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel;

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
        var builder = Kernel.CreateBuilder();
        builder.AddOpenAIChatCompletion("gpt-3.5-turbo", _apiKey);
        var kernel = builder.Build();

        var prompt = @$"I'll pass you question and answer and you have to decide the answer is correct or not. Return only 'true' if correct and 'false' when not.
###
qustion: {question}
answer: {answer}";


        var storyWriter = kernel.CreateFunctionFromPrompt(prompt, executionSettings: new OpenAIPromptExecutionSettings() { MaxTokens = 1024 });
        var result = await kernel.InvokeAsync(storyWriter);
        var content = result.GetValue<string>();
        return bool.Parse(content);
    }
}
