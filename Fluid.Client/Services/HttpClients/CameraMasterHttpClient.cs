using Fluid.Client.Extensions;
using Fluid.Shared.Models;
using Fluid.Shared.Requests;
using Fluid.Shared.Wrapper;
using System.Net.Http.Json;

namespace Fluid.Client.Services.HttpClients;

public class CameraMasterHttpClient
{
    private readonly HttpClient _httpClient;

    public CameraMasterHttpClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<PaginatedResult<CameraModel>> GetAllAsync(PagedRequest pagedRequest)
    {
        var response = await _httpClient.GetAsync("api/masters/cameras".ToPagedRoute(pagedRequest));
        return await response.ToPaginatedResult<CameraModel>();
    }

    public async Task<IResult<CameraModel>> GetByIdAsync(string oemSerialNo)
    {
        var response = await _httpClient.GetAsync($"api/masters/cameras/{oemSerialNo}");
        return await response.ToResult<CameraModel>();
    }

    public async Task<IResult<string>> AddAsync(CameraModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/masters/cameras", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> EditAsync(CameraModel model)
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
