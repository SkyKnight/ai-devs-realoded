using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.Collections.Concurrent;

namespace AiDevReloaded.Api;

public sealed class ConversationService
{
    private readonly IChatCompletionService _chatCompletionService;
    private readonly Kernel _kernel;
    private readonly ConcurrentDictionary<Guid, ChatHistory> _chats = new ConcurrentDictionary<Guid, ChatHistory>();

    private const string SystemPrompt = "Odpowiadaj krótko. Jeśli użytkownik pyta o adres strony to wysyłaj mu tylko sam link, bez żadnego formatowania, niczego więcej nie wyświetlaj.";

    public ConversationService(IChatCompletionService chatCompletionService, Kernel kernel)
    {
        _chatCompletionService = chatCompletionService;
        _kernel = kernel;
    }

    public async Task<string> Ask(string question, Guid conversationId)
    {
        if (!_chats.TryGetValue(conversationId, out var chatHistory))
        {
            chatHistory = new ChatHistory(SystemPrompt);
            _chats.TryAdd(conversationId, chatHistory);
        }
        chatHistory.AddUserMessage(new ChatMessageContentItemCollection
        {
            new TextContent(question)
        });

        OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
        };

        var reply = await _chatCompletionService.GetChatMessageContentAsync(chatHistory, openAIPromptExecutionSettings, _kernel);
        chatHistory.AddAssistantMessage(reply.ToString());
        return reply.ToString();
    }
}
