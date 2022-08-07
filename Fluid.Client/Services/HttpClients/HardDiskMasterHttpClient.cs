using Fluid.Client.Extensions;
using Fluid.Shared.Models;
using Fluid.Shared.Requests;
using Fluid.Shared.Wrapper;
using System.Net.Http.Json;

namespace Fluid.Client.Services.HttpClients;

public class HardDiskMasterHttpClient
{
    private readonly HttpClient _httpClient;

    public HardDiskMasterHttpClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<PaginatedResult<HardDiskModel>> GetAllAsync(PagedRequest pagedRequest)
    {
        var response = await _httpClient.GetAsync("api/masters/harddisks".ToPagedRoute(pagedRequest));
        return await response.ToPaginatedResult<HardDiskModel>();
    }

    public async Task<IResult<HardDiskModel>> GetByIdAsync(string oemSerialNo)
    {
        var response = await _httpClient.GetAsync($"api/masters/harddisks/{oemSerialNo}");
        return await response.ToResult<HardDiskModel>();
    }

    public async Task<IResult<string>> AddAsync(HardDiskModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/masters/harddisks", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> EditAsync(HardDiskModel model)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/masters/harddisks/{model.OemSerialNo}", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> DeleteAsync(string oemSerialNo)
    {
        var response = await _httpClient.DeleteAsync($"api/masters/harddisks/{oemSerialNo}");
        return await response.ToResult<string>();
    }
}
