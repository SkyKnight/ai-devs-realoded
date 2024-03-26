namespace AiDevReloaded.Core.Services;

public sealed class PeopleKnowledgeService
{
    private Dictionary<string, List<string>> _people;

    public PeopleKnowledgeService(IEnumerable<string> knowledge)
    {
        LoadPeople(knowledge);
    }

    public IEnumerable<string> GetPerson(string name)
    {
        if (_people.TryGetValue(name.ToLower(), out var person))
        {
            return person.ToArray();
        }

        return Array.Empty<string>();
    }

    private void LoadPeople(IEnumerable<string> knowledge)
    {
        _people = knowledge
            .Select(x => x.Split(" "))
            .GroupBy(x => x[0])
            .ToDictionary(x => x.Key.ToLower(), x => x.Select(y => string.Join(" ", y[1..])).ToList());
    }
}
