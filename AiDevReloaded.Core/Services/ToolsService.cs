using Microsoft.SemanticKernel;
using Newtonsoft.Json.Linq;

namespace AiDevReloaded.Core.Services;

public sealed class ToolsService
{
    private readonly string _apiKey;

    public ToolsService(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task<string> Ask(string question)
    {
        var prompt = $@"Decide whether the task should be added to the ToDo list or to the calendar (if time is provided) and return the corresponding JSON.
Always use YYYY-MM-DD format for dates. Today is {DateTime.Today.ToString("yyyy-MM-dd")}.
example for ToDo: Przypomnij mi, że mam kupić mleko = {{""tool"":""ToDo"",""desc"":""Kup mleko"" }}
example for Calendar: Jutro mam spotkanie z Marianem = {{""tool"":""Calendar"",""desc"":""Spotkanie z Marianem"",""date"":""2024-04-13""}}
###
{question}";

        var builder = Kernel.CreateBuilder();
        builder.AddOpenAIChatCompletion("gpt-4", _apiKey);
        var kernel = builder.Build();

        var answer = await kernel.InvokePromptAsync<string>(prompt);
        return answer;
    }
}
