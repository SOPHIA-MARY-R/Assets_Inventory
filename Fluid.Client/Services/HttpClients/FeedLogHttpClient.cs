using System.Net.Http.Json;
using Fluid.Client.Extensions;
using Fluid.Shared.Entities;
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
        var response = await _httpClient.PostAsJsonAsync($"api/feed-log?pageNumber={pageNumber}&pageSize={pageSize}", feedLogFilter);
        return await response.ToPaginatedResult<FeedLog>();
    }

    public async Task<IResult> AttendLog(FeedLog feedLog)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/feed-log/{feedLog.Id}/attend", feedLog);
        return await response.ToResult();
    }
    
    public async Task<Result> AutoValidateLogs()
    {
        return await _httpClient.GetFromJsonAsync<Result>("api/feed-log/autovalidate");
    }

}