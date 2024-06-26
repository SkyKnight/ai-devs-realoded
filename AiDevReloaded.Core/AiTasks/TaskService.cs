﻿using Flurl.Http;
using Newtonsoft.Json;

namespace AiDevReloaded.Core.AiTasks;

public sealed class TaskService
{
    private const string TASK_API_URL = "https://tasks.aidevs.pl/";

    public async Task<string> GetToken(string apiKey, string taskName, CancellationToken cancellationToken = default)
    {
        var response = await (TASK_API_URL + "token/" + taskName)
            .PostJsonAsync(new { apikey = apiKey }, cancellationToken: cancellationToken)
            .ReceiveJson<TokenResponse>();

        CheckResponse(response);
        return response.Token;
    }

    public async Task<T> GetTaskContent<T>(string token, CancellationToken cancellationToken = default) where T : ApiResponse, new()
    {
        var response = await (TASK_API_URL + "task/" + token)
            .GetJsonAsync<T>(cancellationToken: cancellationToken);

        CheckResponse(response);
        return response;
    }

    public async Task<T> PostTaskContent<T>(string token, IReadOnlyDictionary<string, string> content, CancellationToken cancellationToken = default) where T : ApiResponse, new()
    {
        var response = await (TASK_API_URL + "task/" + token)
            .PostMultipartAsync(cnt => content.ToList().ForEach(x => cnt.AddString(x.Key, x.Value)), cancellationToken: cancellationToken)
            .ReceiveJson<T>();

        CheckResponse(response);
        return response;
    }

    public async Task SendAnswer<T>(string token, T answer, CancellationToken cancellationToken = default)
    {
        AnswerResponse response;
        try
        {
            response = await (TASK_API_URL + "answer/" + token)
            .PostJsonAsync(new { answer })
            .ReceiveJson<AnswerResponse>();
        }
        catch (FlurlHttpException ex)
        {
            var error = await ex.GetResponseStringAsync();
            throw new Exception($"Error: {ex.Message}. {error}.", ex);
        }

        CheckResponse(response);
    }

    private void CheckResponse(ApiResponse? response)
    {
        if (response is null)
        {
            throw new Exception("Response is null");
        }

        if (response.Code != 0)
        {
            throw new Exception($"Error: {response.Message}");
        }
    }
}
