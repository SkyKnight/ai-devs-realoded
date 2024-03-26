using System.Runtime.CompilerServices;

namespace AiDevReloaded.Core.Services;

public sealed class PeopleService
{
    private readonly PeopleKnowledgeService _peopleKnowledgeService;
    private readonly string _apiKey;

    public PeopleService(PeopleKnowledgeService peopleKnowledgeService, string apiKey)
    {
        _peopleKnowledgeService = peopleKnowledgeService;
        _apiKey = apiKey;
    }

    public async Task<string> GetPersonName(string question)
    {
        return await Task.FromResult("John Doe");
    }

    public async Task<string> Ask(string question, string personName)
    {
        return await Task.FromResult("John Doe is a person.");
    }
}
