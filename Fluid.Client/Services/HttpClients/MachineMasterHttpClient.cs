using Fluid.Client.Extensions;
using Fluid.Shared.Models;
using Fluid.Shared.Requests;
using Fluid.Shared.Wrapper;
using System.Net.Http.Json;
using Fluid.Shared.Entities;

namespace Fluid.Client.Services.HttpClients;

public class MachineMasterHttpClient
{
    private readonly HttpClient _httpClient;

    public MachineMasterHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PaginatedResult<MachineInfo>> GetAllAsync(PagedRequest pagedRequest)
    {
        return await _httpClient.GetFromJsonAsync<PaginatedResult<MachineInfo>>("api/masters/machines".ToPagedRoute(pagedRequest));
    }

    public async Task<Result<SystemConfiguration>> GetByIdAsync(string assetTag)
    {
        return await _httpClient.GetFromJsonAsync<Result<SystemConfiguration>>($"api/masters/machines/{assetTag}");
    }

    public async Task<IResult<string>> AddAsync(SystemConfiguration model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/masters/machines", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> EditAsync(SystemConfiguration model)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/masters/machines/{model.MachineDetails.AssetTag}", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> DeleteAsync(string assetTag)
    {
        var response = await _httpClient.DeleteAsync($"api/masters/machines/{assetTag}");
        return await response.ToResult<string>();
    }
}
