using Fluid.Client.Extensions;
using Fluid.Shared.Models;
using Fluid.Shared.Requests;
using Fluid.Shared.Wrapper;
using System.Net.Http.Json;
using Fluid.Shared.Entities;

namespace Fluid.Client.Services.HttpClients;

public class ProcessorMasterHttpClient
{
    private readonly HttpClient _httpClient;

    public ProcessorMasterHttpClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<PaginatedResult<ProcessorInfo>> GetAllAsync(PagedRequest pagedRequest)
    {
        var response = await _httpClient.GetAsync("api/masters/processors".ToPagedRoute(pagedRequest));
        return await response.ToPaginatedResult<ProcessorInfo>();
    }

    public async Task<IResult<ProcessorInfo>> GetByIdAsync(string oemSerialNo)
    {
        var response = await _httpClient.GetAsync($"api/masters/processors/{oemSerialNo}");
        return await response.ToResult<ProcessorInfo>();
    }

    public async Task<IResult<string>> AddAsync(ProcessorInfo model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/masters/processors", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> EditAsync(ProcessorInfo model)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/masters/processors/{model.ProcessorId}", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> DeleteAsync(string oemSerialNo)
    {
        var response = await _httpClient.DeleteAsync($"api/masters/processors/{oemSerialNo}");
        return await response.ToResult<string>();
    }
}
