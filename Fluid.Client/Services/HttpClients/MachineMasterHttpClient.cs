using Fluid.Client.Extensions;
using Fluid.Shared.Models;
using Fluid.Shared.Requests;
using Fluid.Shared.Wrapper;
using System.Net.Http.Json;

namespace Fluid.Client.Services.HttpClients;

public class MachineMasterHttpClient
{
    private readonly HttpClient _httpClient;

    public MachineMasterHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PaginatedResult<MachineMasterModel>> GetAllAsync(PagedRequest pagedRequest)
    {
        return await _httpClient.GetFromJsonAsync<PaginatedResult<MachineMasterModel>>("api/masters/machines".ToPagedRoute(pagedRequest));
    }

    public async Task<Result<MachineMasterModel>> GetByIdAsync(string assetTag)
    {
        return await _httpClient.GetFromJsonAsync<Result<MachineMasterModel>>($"api/masters/machines/{assetTag}");
    }

    public async Task<IResult<string>> AddAsync(MachineMasterModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/masters/machines", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> EditAsync(MachineMasterModel model)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/masters/machines/{model.AssetTag}", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> DeleteAsync(string assetTag)
    {
        var response = await _httpClient.DeleteAsync($"api/masters/machines/{assetTag}");
        return await response.ToResult<string>();
    }
}
