using Flurl.Http;
using Newtonsoft.Json.Linq;

namespace AiDevReloaded.Core.RenderForm;

public sealed class RenderFormService
{
    private readonly  string _apiKey;

    private const string _baseUrl = "https://get.renderform.io/api/v2/render";
    private const string _templateId = "needy-devils-retire-promptly-1438";

    public RenderFormService(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task<string> GenerateMeme(string text, string image)
    {
        try
        {
            var result = await _baseUrl.WithHeader("X-API-KEY", _apiKey).PostJsonAsync(new
            {
                template = _templateId,
                data = new Dictionary<string, string>
            {
                { "title.text", text },
                { "image.src", image }
            }
            });
            var json = await result.GetJsonAsync<Dictionary<string, object>>();
            return json["href"].ToString();
        }
        catch (FlurlHttpException ex)
        {
            throw new Exception(await ex.GetResponseStringAsync(), ex);
            throw;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
