using Newtonsoft.Json;
using System.Collections.Generic;

namespace AiDevReloaded.Core.OpenAI.Moderation;

public class Response
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("model")]
    public string Model { get; set; }

    [JsonProperty("results")]
    public List<Result> Results { get; set; }
}

public class Result
{
    [JsonProperty("flagged")]
    public bool Flagged { get; set; }

    [JsonProperty("categories")]
    public Categories Categories { get; set; }

    [JsonProperty("category_scores")]
    public CategoryScores CategoryScores { get; set; }
}

public class Categories
{
    [JsonProperty("sexual")]
    public bool Sexual { get; set; }

    [JsonProperty("hate")]
    public bool Hate { get; set; }

    [JsonProperty("harassment")]
    public bool Harassment { get; set; }

    [JsonProperty("self-harm")]
    public bool SelfHarm { get; set; }

    [JsonProperty("sexual/minors")]
    public bool SexualMinors { get; set; }

    [JsonProperty("hate/threatening")]
    public bool HateThreatening { get; set; }

    [JsonProperty("violence/graphic")]
    public bool ViolenceGraphic { get; set; }

    [JsonProperty("self-harm/intent")]
    public bool SelfHarmIntent { get; set; }

    [JsonProperty("self-harm/instructions")]
    public bool SelfHarmInstructions { get; set; }

    [JsonProperty("harassment/threatening")]
    public bool HarassmentThreatening { get; set; }

    [JsonProperty("violence")]
    public bool Violence { get; set; }
}

public class CategoryScores
{
    [JsonProperty("sexual")]
    public double Sexual { get; set; }

    [JsonProperty("hate")]
    public double Hate { get; set; }

    [JsonProperty("harassment")]
    public double Harassment { get; set; }

    [JsonProperty("self-harm")]
    public double SelfHarm { get; set; }

    [JsonProperty("sexual/minors")]
    public double SexualMinors { get; set; }

    [JsonProperty("hate/threatening")]
    public double HateThreatening { get; set; }

    [JsonProperty("violence/graphic")]
    public double ViolenceGraphic { get; set; }

    [JsonProperty("self-harm/intent")]
    public double SelfHarmIntent { get; set; }

    [JsonProperty("self-harm/instructions")]
    public double SelfHarmInstructions { get; set; }

    [JsonProperty("harassment/threatening")]
    public double HarassmentThreatening { get; set; }

    [JsonProperty("violence")]
    public double Violence { get; set; }
}
