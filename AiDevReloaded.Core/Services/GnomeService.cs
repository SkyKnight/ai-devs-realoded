using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace AiDevReloaded.Core.Services;

public sealed class GnomeService
{
    private readonly string _apiKey;

    public GnomeService(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task<string> GetHatColor(string gnomePictureUrl)
    {
        // https://systenics.ai/blog/2024-02-19-vision-with-semantic-kernel/
        var builder = Kernel.CreateBuilder();

        builder.AddOpenAIChatCompletion("gpt-4-vision-preview", _apiKey);
        var kernel = builder.Build();
        var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

        var prompt = $"What color is the hat of the gnome in the picture at {gnomePictureUrl}?";
        var chatHistory = new ChatHistory();

        chatHistory.AddUserMessage(new ChatMessageContentItemCollection
        {
            new TextContent("I will give you a drawing of a gnome with a hat on his head. " +
                            "Tell me what is the color of the hat in POLISH. " +
                            "If any errors occur (for example. on image there is no gnome, or gnome has no hat), " +
                            "return 'ERROR' as answer." +
                            "Answer only with color or error. No additional text."),
            new ImageContent(new Uri(gnomePictureUrl))
        });

        var reply = await chatCompletionService.GetChatMessageContentAsync(chatHistory);
        return reply.ToString();
    }
}
