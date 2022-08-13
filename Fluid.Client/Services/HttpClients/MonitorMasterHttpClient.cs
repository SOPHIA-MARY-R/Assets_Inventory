using Fluid.Client.Extensions;
using Fluid.Shared.Models;
using Fluid.Shared.Requests;
using Fluid.Shared.Wrapper;
using System.Net.Http.Json;

namespace Fluid.Client.Services.HttpClients;

public class MonitorMasterHttpClient
{
    private readonly HttpClient _httpClient;

    public MonitorMasterHttpClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<PaginatedResult<MonitorModel>> GetAllAsync(PagedRequest pagedRequest)
    {
        var response = await _httpClient.GetAsync("api/masters/monitors".ToPagedRoute(pagedRequest));
        return await response.ToPaginatedResult<MonitorModel>();
    }

    public async Task<IResult<MonitorModel>> GetByIdAsync(string oemSerialNo)
    {
        var response = await _httpClient.GetAsync($"api/masters/monitors/{oemSerialNo}");
        return await response.ToResult<MonitorModel>();
    }

    public async Task<IResult<string>> AddAsync(MonitorModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/masters/monitors", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> EditAsync(MonitorModel model)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/masters/monitors/{model.OemSerialNo}", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> DeleteAsync(string oemSerialNo)
    {
        var response = await _httpClient.DeleteAsync($"api/masters/monitors/{oemSerialNo}");
        return await response.ToResult<string>();
    }
}
