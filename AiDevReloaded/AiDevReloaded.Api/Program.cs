using AiDevReloaded.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(_ =>
{
    var builder = Kernel.CreateBuilder();
    builder.AddOpenAIChatCompletion("gpt-4", Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? throw new Exception("OPENAI_API_KEY is not set"));
    var kernel = builder.Build();
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
    var answer = await conversationService.Ask(request.Question);
    logger.LogInformation("Answer: {Answer}", answer);
    return new { reply = answer };
});

app.Run();

class Request
{
    public string Question { get; set; } = string.Empty;
}