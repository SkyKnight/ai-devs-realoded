using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Newtonsoft.Json.Linq;

namespace AiDevReloaded.Tests._04_Blogger;

public sealed class PizzaStoryWriter
{
    private readonly string _apiKey;

    public PizzaStoryWriter(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task<IEnumerable<string>> WriteStory(IEnumerable<string> chapters)
    {
        var builder = Kernel.CreateBuilder();
        builder.AddOpenAIChatCompletion("gpt-3.5-turbo", _apiKey);
        var kernel = builder.Build();

        var chaptersStr = string.Join(Environment.NewLine, chapters);

        var prompt = @$"Jesteś specjalistą od pizzy i jej historii. Piszesz krótko i zwięźle. To jest lista rozdziałów, które musisz napisać:       
{chaptersStr}
Zwracasz je w formacie:
Tytuł rozdziału
Treść rodziału
@
Tytuł rozdziału
Treść rodziału
@
";


        var storyWriter = kernel.CreateFunctionFromPrompt(prompt, executionSettings: new OpenAIPromptExecutionSettings() { MaxTokens = 1024 });
        var result = await kernel.InvokeAsync(storyWriter);
        var content = result.GetValue<string>();
        return content.Split('@', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());
    }
}
