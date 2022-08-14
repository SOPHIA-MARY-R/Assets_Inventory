using Fluid.Client.Extensions;
using Fluid.Shared.Models;
using Fluid.Shared.Requests;
using Fluid.Shared.Wrapper;
using System.Net.Http.Json;

namespace Fluid.Client.Services.HttpClients;

public class SpeakerMasterHttpClient
{
    private readonly HttpClient _httpClient;

    public SpeakerMasterHttpClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<PaginatedResult<SpeakerModel>> GetAllAsync(PagedRequest pagedRequest)
    {
        var response = await _httpClient.GetAsync("api/masters/speakers".ToPagedRoute(pagedRequest));
        return await response.ToPaginatedResult<SpeakerModel>();
    }

    public async Task<IResult<SpeakerModel>> GetByIdAsync(string oemSerialNo)
    {
        var response = await _httpClient.GetAsync($"api/masters/speakers/{oemSerialNo}");
        return await response.ToResult<SpeakerModel>();
    }

    public async Task<IResult<string>> AddAsync(SpeakerModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/masters/speakers", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> EditAsync(SpeakerModel model)
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
