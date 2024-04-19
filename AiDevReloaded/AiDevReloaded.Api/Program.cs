using AiDevReloaded.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Plugins.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(_ =>
{
    var bldr = Kernel.CreateBuilder();
    bldr.Services.AddLogging();
    bldr.Services.ConfigureHttpClientDefaults(x => x.AddDefaultLogger());
    bldr.AddOpenAIChatCompletion("gpt-4", Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? throw new Exception("OPENAI_API_KEY is not set"));
#pragma warning disable SKEXP0050 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
    bldr.Plugins.AddFromType<TimePlugin>();
#pragma warning restore SKEXP0050 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
    bldr.Plugins.AddFromType<SerpApiPlugin>();
    return bldr.Build();
});
builder.Services.AddSingleton(x =>
{
    var kernel = x.GetRequiredService<Kernel>();
    return kernel.GetRequiredService<IChatCompletionService>();
});
builder.Services.AddSingleton<ConversationService>();

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();


app.MapPost("/answer", async ([FromBody]Request request, ConversationService conversationService, ILogger<Request> logger) =>
{
    logger.LogInformation("Question: {Question}", request.Question);
    var conversationId = request.ConversationId ?? Guid.NewGuid();
    var answer = await conversationService.Ask(request.Question, conversationId);
    logger.LogInformation("Answer: {Answer}", answer);
    return new { reply = answer, conversationId };
});

app.Run();

class Request
{
    public string Question { get; set; } = string.Empty;

    public Guid? ConversationId { get; set; }
}