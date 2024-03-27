namespace AiDevReloaded.Core.OpenAI.Embedding;

using Newtonsoft.Json;
using System.Collections.Generic;

public class Response
{
    [JsonProperty("object")]
    public string Object { get; set; }

    [JsonProperty("data")]
    public List<DataItem> Data { get; set; }

    [JsonProperty("model")]
    public string Model { get; set; }

    [JsonProperty("usage")]
    public Usage Usage { get; set; }
}

public class DataItem
{
    [JsonProperty("object")]
    public string Object { get; set; }

    [JsonProperty("index")]
    public int Index { get; set; }

    [JsonProperty("embedding")]
    public List<double> Embedding { get; set; }
}

public class Usage
{
    [JsonProperty("prompt_tokens")]
    public int PromptTokens { get; set; }

    [JsonProperty("total_tokens")]
    public int TotalTokens { get; set; }
}
