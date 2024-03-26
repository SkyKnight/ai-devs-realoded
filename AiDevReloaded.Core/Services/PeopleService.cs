using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.Runtime.CompilerServices;

namespace AiDevReloaded.Core.Services;

public sealed class PeopleService
{
    private readonly PeopleKnowledgeService _peopleKnowledgeService;
    private readonly string _apiKey;

    private readonly Lazy<Kernel> _kernel;

    private Kernel Kernel => _kernel.Value;

    public PeopleService(PeopleKnowledgeService peopleKnowledgeService, string apiKey)
    {
        _peopleKnowledgeService = peopleKnowledgeService;
        _apiKey = apiKey;
        _kernel = new(() =>
        {
            var builder = Kernel.CreateBuilder();
            builder.AddOpenAIChatCompletion("gpt-3.5-turbo", _apiKey);
            return builder.Build();
        });
    }

    public async Task<string> GetPersonName(string question)
    {
        var prompt = $@"Z przekazanego tekstu wyodrębnij i wypisz tylko imię osoby. Nie pisz niczego ponad to.
###
{question}";

        var storyWriter = Kernel.CreateFunctionFromPrompt(prompt, executionSettings: new OpenAIPromptExecutionSettings() { MaxTokens = 1024 });
        var result = await Kernel.InvokeAsync(storyWriter);
        return result.GetValue<string>();
    }

    public async Task<string> Ask(string question, string personName)
    {
        var person = _peopleKnowledgeService.GetPerson(personName);

        var prompt = $@"Korzystając z przekazanego kontekstu odpowiedz na pytanie związane z tą osobą. Na wszystkie inne odpowiadaj pomidor.
###
{string.Join(Environment.NewLine, person)}";

        var storyWriter = Kernel.CreateFunctionFromPrompt(prompt, executionSettings: new OpenAIPromptExecutionSettings() { MaxTokens = 1024 });
        var result = await Kernel.InvokeAsync(storyWriter);
        return result.GetValue<string>();
    }
}
