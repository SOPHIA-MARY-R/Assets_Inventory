using System.Net.Http.Json;
using Fluid.Client.Extensions;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;
using Fluid.Shared.Models.FilterModels;
using Fluid.Shared.Requests;
using Fluid.Shared.Wrapper;

namespace Fluid.Client.Services.HttpClients;

public class FeedLogHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly PeriodService _periodService;

    public FeedLogHttpClient(HttpClient httpClient, PeriodService periodService)
    {
        _httpClient = httpClient;
        _periodService = periodService;
    }

    public async Task<Result<FeedLog>> GetByIdAsync(string id)
    {
        return await _httpClient.GetFromJsonAsync<Result<FeedLog>>($"api/feed-log/{id}");
    }

    public async Task<PaginatedResult<FeedLog>> GetAllAsync(int pageNumber, int pageSize, FeedLogFilter feedLogFilter)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/feed-log?pageNumber={pageNumber}&pageSize={pageSize}",
            feedLogFilter);
        return await response.ToPaginatedResult<FeedLog>();
    }

    public async Task<Result> AutoValidateLogs()
    {
        return await _httpClient.GetFromJsonAsync<Result>("api/feed-log/autovalidate");
    }

    public async Task<Result<FeedLogCountDetails>> GetCountDetails()
    {
        return await _httpClient.GetFromJsonAsync<Result<FeedLogCountDetails>>("api/feed-log/count-details");
    }

    public async Task<IResult> AcceptAsync(string id, string remarks)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/feed-log/{id}/accept",
            new AcceptChangeRequest() { Id = id, Remarks = remarks });
        return await response.ToResult();
    }

    public async Task<Result> IgnoreAsync(string id)
    {
        return await _httpClient.GetFromJsonAsync<Result>($"api/feed-log/{id}/ignore");
    }
}