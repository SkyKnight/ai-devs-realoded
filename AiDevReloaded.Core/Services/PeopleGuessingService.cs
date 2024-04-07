using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel;

namespace AiDevReloaded.Core.Services;

public sealed class PeopleGuessingService
{
    private readonly string _apiKey;

    public PeopleGuessingService(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task<string> Guess(Func<Task<string>> hint)
    {
        var builder = Kernel.CreateBuilder();
        builder.AddOpenAIChatCompletion("gpt-4", _apiKey);
        var kernel = builder.Build();

        var hints = new List<string>();

        while(true)
        {
            hints.Add(await hint());

            var prompt = @$"Jesteś specjalistą od ludzi. Zgadnij o kogo chodzi. Podaj imię i nazwisko. Jest to znana osoba. Jeśli nie wiesz to odpowiedz NIE. Podpowiedzi:
{string.Join(Environment.NewLine, hints)}
";

            var storyWriter = kernel.CreateFunctionFromPrompt(prompt, executionSettings: new OpenAIPromptExecutionSettings() { MaxTokens = 1024 });
            var result = await kernel.InvokeAsync(storyWriter);
            var content = result.GetValue<string>();
            if (!content.Equals("NIE"))
            {
                return content;
            }
        }
    }
}
