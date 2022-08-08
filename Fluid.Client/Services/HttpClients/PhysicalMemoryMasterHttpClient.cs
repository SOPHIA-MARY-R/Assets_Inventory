using Fluid.Client.Extensions;
using Fluid.Shared.Models;
using Fluid.Shared.Requests;
using Fluid.Shared.Wrapper;
using System.Net.Http.Json;

namespace Fluid.Client.Services.HttpClients;

public class PhysicalMemoryMasterHttpClient
{
    private readonly HttpClient _httpClient;

    public PhysicalMemoryMasterHttpClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<PaginatedResult<PhysicalMemoryModel>> GetAllAsync(PagedRequest pagedRequest)
    {
        var response = await _httpClient.GetAsync("api/masters/physicalmemorys".ToPagedRoute(pagedRequest));
        return await response.ToPaginatedResult<PhysicalMemoryModel>();
    }

    public async Task<IResult<PhysicalMemoryModel>> GetByIdAsync(string oemSerialNo)
    {
        var response = await _httpClient.GetAsync($"api/masters/physicalmemorys/{oemSerialNo}");
        return await response.ToResult<PhysicalMemoryModel>();
    }

    public async Task<IResult<string>> AddAsync(PhysicalMemoryModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/masters/physicalmemorys", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> EditAsync(PhysicalMemoryModel model)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/masters/physicalmemorys/{model.OemSerialNo}", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> DeleteAsync(string oemSerialNo)
    {
        var response = await _httpClient.DeleteAsync($"api/masters/physicalmemorys/{oemSerialNo}");
        return await response.ToResult<string>();
    }
}
