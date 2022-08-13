using Fluid.Client.Extensions;
using Fluid.Shared.Models;
using Fluid.Shared.Requests;
using Fluid.Shared.Wrapper;
using System.Net.Http.Json;

namespace Fluid.Client.Services.HttpClients;

public class ProcessorMasterHttpClient
{
    private readonly HttpClient _httpClient;

    public ProcessorMasterHttpClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<PaginatedResult<ProcessorModel>> GetAllAsync(PagedRequest pagedRequest)
    {
        var response = await _httpClient.GetAsync("api/masters/processors".ToPagedRoute(pagedRequest));
        return await response.ToPaginatedResult<ProcessorModel>();
    }

    public async Task<IResult<ProcessorModel>> GetByIdAsync(string processorId)
    {
        var response = await _httpClient.GetAsync($"api/masters/processors/{processorId}");
        return await response.ToResult<ProcessorModel>();
    }

    public async Task<IResult<string>> AddAsync(ProcessorModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/masters/processors", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> EditAsync(ProcessorModel model)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/masters/processors/{model.ProcessorId}", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> DeleteAsync(string processorId)
    {
        var response = await _httpClient.DeleteAsync($"api/masters/processors/{processorId}");
        return await response.ToResult<string>();
    }
}
