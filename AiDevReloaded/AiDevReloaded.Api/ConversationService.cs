using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace AiDevReloaded.Api;

public sealed class ConversationService
{
    private readonly IChatCompletionService _chatCompletionService;
    private readonly ChatHistory _chatHistory = new ChatHistory();

    public ConversationService(IChatCompletionService chatCompletionService)
    {
        _chatCompletionService = chatCompletionService;
    }

    public async Task<string> Ask(string question)
    {
        _chatHistory.AddUserMessage(new ChatMessageContentItemCollection
        {
            new TextContent(question)
        });
        var reply = await _chatCompletionService.GetChatMessageContentAsync(_chatHistory);
        _chatHistory.AddAssistantMessage(reply.ToString());
        return reply.ToString();
    }
}
