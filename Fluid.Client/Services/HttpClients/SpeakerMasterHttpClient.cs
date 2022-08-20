using Fluid.Client.Extensions;
using Fluid.Shared.Models;
using Fluid.Shared.Requests;
using Fluid.Shared.Wrapper;
using System.Net.Http.Json;
using Fluid.Shared.Entities;

namespace Fluid.Client.Services.HttpClients;

public class SpeakerMasterHttpClient
{
    private readonly HttpClient _httpClient;

    public SpeakerMasterHttpClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<PaginatedResult<SpeakerInfo>> GetAllAsync(PagedRequest pagedRequest)
    {
        var response = await _httpClient.GetAsync("api/masters/speakers".ToPagedRoute(pagedRequest));
        return await response.ToPaginatedResult<SpeakerInfo>();
    }

    public async Task<IResult<SpeakerInfo>> GetByIdAsync(string oemSerialNo)
    {
        var response = await _httpClient.GetAsync($"api/masters/speakers/{oemSerialNo}");
        return await response.ToResult<SpeakerInfo>();
    }

    public async Task<IResult<string>> AddAsync(SpeakerInfo model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/masters/speakers", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> EditAsync(SpeakerInfo model)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/masters/speakers/{model.OemSerialNo}", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> DeleteAsync(string oemSerialNo)
    {
        var response = await _httpClient.DeleteAsync($"api/masters/speakers/{oemSerialNo}");
        return await response.ToResult<string>();
    }
}
