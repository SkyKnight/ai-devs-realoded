using Flurl;
using Flurl.Http;
using Microsoft.SemanticKernel;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace AiDevReloaded.Api;

public sealed class SerpApiPlugin
{
    private const string Url = "https://serpapi.com/search.json";


    [KernelFunction]
    [Description("Get search results from the Web")]
    public async Task<string> GetSearchResults(
        [Description("Parameter defines the query you want to search. You can use anything that you would use in a regular Google search. e.g. inurl:, site:, intitle:. We also support advanced search query parameters such as as_dt and as_eq..")]string query,
        [Description("Parameter defines the language to use for the Google search. It's a two-letter language code. (e.g., en for English, es for Spanish, or fr for French). ")]string language)
    {
        var apiKey = Environment.GetEnvironmentVariable("SERP_API_KEY");
        var response = await Url.SetQueryParams(new
        {
            q = query,
            api_key = apiKey,
            hl = language,
            num = 5
        }).GetStringAsync();
        var result = JObject.Parse(response);
        return result["organic_results"].ToString();
    }
}
