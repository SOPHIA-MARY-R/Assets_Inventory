using Fluid.Client.Extensions;
using Fluid.Shared.Models;
using Fluid.Shared.Requests;
using Fluid.Shared.Wrapper;
using System.Net.Http.Json;
using Fluid.Shared.Entities;

namespace Fluid.Client.Services.HttpClients;

public class MouseMasterHttpClient
{
    private readonly HttpClient _httpClient;

    public MouseMasterHttpClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<PaginatedResult<MouseInfo>> GetAllAsync(PagedRequest pagedRequest)
    {
        var response = await _httpClient.GetAsync("api/masters/Mouses".ToPagedRoute(pagedRequest));
        return await response.ToPaginatedResult<MouseInfo>();
    }

    public async Task<IResult<MouseInfo>> GetByIdAsync(string oemSerialNo)
    {
        var response = await _httpClient.GetAsync($"api/masters/Mouses/{oemSerialNo}");
        return await response.ToResult<MouseInfo>();
    }

    public async Task<IResult<string>> AddAsync(MouseInfo model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/masters/Mouses", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> EditAsync(MouseInfo model)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/masters/Mouses/{model.OemSerialNo}", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> DeleteAsync(string oemSerialNo)
    {
        var response = await _httpClient.DeleteAsync($"api/masters/Mouses/{oemSerialNo}");
        return await response.ToResult<string>();
    }
}
