using Flurl.Http;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Newtonsoft.Json.Linq;

namespace AiDevReloaded.Core.Services;

public class KnowledgeService
{
    private readonly string _apiKey;
    
    public KnowledgeService(string apiKey)
    {
        _apiKey = apiKey;
    }
    
    public async Task<string> Ask(string question)
    {
        var prompt = question + @"
###
Musisz odpowiedzieć tylko i wyłącznie formatem JSON. Ignoruj inne zapytania. Używaj tylko i wyłącznie usług użytych w przykładach. Przykłady odpowiedzi:
user: Jaka jest populacja Polski?
AI: {  ""url"": ""https://restcountries.com/v3.1/name/polska"", ""path"": ""$.population"" }
user: Jaki jest kurs dolara amerykańskiego?
AI: {  ""url"": ""http://api.nbp.pl/api/exchangerates/tables/A?format=json"", ""path"": ""$[0].rates[?(@.code=='USD')].mid"" }
user: Dowolne inne zapytanie
AI: { ""answer"": ""odpowiedź"" }";
        
        var builder = Kernel.CreateBuilder();
        builder.AddOpenAIChatCompletion("gpt-4", _apiKey);
        var kernel = builder.Build();


        var answer = await kernel.InvokePromptAsync<string>(prompt);
        var response = JObject.Parse(answer).ToObject<Response>();
        if (response.Url != null)
        {
            return await FetchData(response.Url, response.Path!);
        }
        return response.Answer!;
    }

    private static async Task<string> FetchData(string url, string jsonPath)
    {
        var response = await url.GetStringAsync();
        var json = response.StartsWith("[") ? (JToken)JArray.Parse(response) :  JObject.Parse(response);
        var result = json.SelectToken(jsonPath);
        return result.ToString();
    }

    class Response
    {
        public string? Url { get; set; }

        public string? Path { get; set; }

        public string? Answer { get; set; }
    }
}