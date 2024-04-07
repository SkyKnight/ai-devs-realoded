using AiDevReloaded.Core.OpenAI.Embedding;
using Azure.AI.OpenAI;
using Google.Protobuf;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Qdrant.Client;
using Qdrant.Client.Grpc;

namespace AiDevReloaded.Core.Services;

public sealed class ArchiveService
{
    private readonly string _apiKey;
    private readonly string _archiveFilePath;
    private readonly Dictionary<Guid, ArchiveEntry> _archiveEntries = new Dictionary<Guid, ArchiveEntry>();
    private readonly QdrantClient _client;
    private readonly EmbeddingService _embeddingService;
    private readonly string _collectionName = "archive-" + Guid.NewGuid().ToString();

    public ArchiveService(string apiKey, string archiveFilePath)
    {
        _apiKey = apiKey;
        _archiveFilePath = archiveFilePath;
        _client = new QdrantClient("localhost");
        _embeddingService = new EmbeddingService(apiKey);
    }

    public async Task<string?> FindUrl(string question)
    {
        await EnsureArchiveLoaded();
        var questionEmbedding = await _embeddingService.GetEmbedding(question);

        var filter = new Qdrant.Client.Grpc.Filter();
        //filter.Must.Add(new Qdrant.Client.Grpc.Condition())
        var result = await _client.SearchAsync(_collectionName, questionEmbedding);
        var r = result.FirstOrDefault();
        return r?.Payload["url"]?.StringValue;
    }

    private async Task EnsureArchiveLoaded()
    {
        if (_archiveEntries.Count == 0)
        {
            await LoadArchive();
        }
    }

    private async Task LoadArchive()
    {
        await _client.CreateCollectionAsync(_collectionName, new VectorParams() { Distance = Distance.Cosine, OnDisk = true, Size = 1536 });
        var jsonData = await File.ReadAllTextAsync(_archiveFilePath);
        var archiveEntries = JArray.Parse(jsonData).ToObject<List<ArchiveEntry>>();
        var points = new List<PointStruct>();
        foreach (var entry in archiveEntries)
        {
            var id = Guid.NewGuid();
            _archiveEntries.Add(id, entry);
            var embedding = await _embeddingService.GetEmbedding(entry.Title + Environment.NewLine + entry.Info);
            points.Add(new PointStruct()
            {
                Id = new PointId() { Uuid = id.ToString() },
                Vectors = new Vectors() { Vector = new Vector() { Data = { embedding } } },
                Payload = 
                {
                    { "url", new Value() { StringValue = entry.Url } }
                }
            });
        }
        await _client.UpsertAsync(_collectionName, points);
    }

    class ArchiveEntry
    {
        public string Title { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public string Info { get; set; } = string.Empty;
    }
}
