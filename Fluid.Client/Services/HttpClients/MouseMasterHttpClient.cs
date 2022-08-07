using Fluid.Client.Extensions;
using Fluid.Shared.Models;
using Fluid.Shared.Requests;
using Fluid.Shared.Wrapper;
using System.Net.Http.Json;

namespace Fluid.Client.Services.HttpClients;

public class MouseMasterHttpClient
{
    private readonly HttpClient _httpClient;

    public MouseMasterHttpClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<PaginatedResult<MouseModel>> GetAllAsync(PagedRequest pagedRequest)
    {
        var response = await _httpClient.GetAsync("api/masters/Mouses".ToPagedRoute(pagedRequest));
        return await response.ToPaginatedResult<MouseModel>();
    }

    public async Task<IResult<MouseModel>> GetByIdAsync(string oemSerialNo)
    {
        var response = await _httpClient.GetAsync($"api/masters/Mouses/{oemSerialNo}");
        return await response.ToResult<MouseModel>();
    }

    public async Task<IResult<string>> AddAsync(MouseModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/masters/Mouses", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> EditAsync(MouseModel model)
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
