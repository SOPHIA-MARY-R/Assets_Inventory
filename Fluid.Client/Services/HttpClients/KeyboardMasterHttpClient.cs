using Fluid.Client.Extensions;
using Fluid.Shared.Models;
using Fluid.Shared.Requests;
using Fluid.Shared.Wrapper;
using System.Net.Http.Json;

namespace Fluid.Client.Services.HttpClients;

public class KeyboardMasterHttpClient
{
    private readonly HttpClient _httpClient;

    public KeyboardMasterHttpClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<PaginatedResult<KeyboardModel>> GetAllAsync(PagedRequest pagedRequest)
    {
        var response = await _httpClient.GetAsync("api/masters/keyboards".ToPagedRoute(pagedRequest));
        return await response.ToPaginatedResult<KeyboardModel>();
    }

    public async Task<IResult<KeyboardModel>> GetByIdAsync(string oemSerialNo)
    {
        var response = await _httpClient.GetAsync($"api/masters/keyboards/{oemSerialNo}");
        return await response.ToResult<KeyboardModel>();
    }

    public async Task<IResult<string>> AddAsync(KeyboardModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/masters/keyboards", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> EditAsync(KeyboardModel model)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/masters/keyboards/{model.OemSerialNo}", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> DeleteAsync(string oemSerialNo)
    {
        var response = await _httpClient.DeleteAsync($"api/masters/keyboards/{oemSerialNo}");
        return await response.ToResult<string>();
    }
}
