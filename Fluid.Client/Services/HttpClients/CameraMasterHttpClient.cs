using Fluid.Client.Extensions;
using Fluid.Shared.Models;
using Fluid.Shared.Requests;
using Fluid.Shared.Wrapper;
using System.Net.Http.Json;
using Fluid.Shared.Entities;

namespace Fluid.Client.Services.HttpClients;

public class CameraMasterHttpClient
{
    private readonly HttpClient _httpClient;

    public CameraMasterHttpClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<PaginatedResult<CameraInfo>> GetAllAsync(PagedRequest pagedRequest)
    {
        var response = await _httpClient.GetAsync("api/masters/cameras".ToPagedRoute(pagedRequest));
        return await response.ToPaginatedResult<CameraInfo>();
    }

    public async Task<IResult<CameraInfo>> GetByIdAsync(string oemSerialNo)
    {
        var response = await _httpClient.GetAsync($"api/masters/cameras/{oemSerialNo}");
        return await response.ToResult<CameraInfo>();
    }

    public async Task<IResult<string>> AddAsync(CameraInfo model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/masters/cameras", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> EditAsync(CameraInfo model)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/masters/cameras/{model.OemSerialNo}", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> DeleteAsync(string oemSerialNo)
    {
        var response = await _httpClient.DeleteAsync($"api/masters/cameras/{oemSerialNo}");
        return await response.ToResult<string>();
    }
}
