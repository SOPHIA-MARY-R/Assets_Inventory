using Fluid.Client.Extensions;
using Fluid.Shared.Models;
using Fluid.Shared.Requests;
using Fluid.Shared.Wrapper;
using System.Net.Http.Json;

namespace Fluid.Client.Services.HttpClients;

public class GraphicsCardMasterHttpClient
{
    private readonly HttpClient _httpClient;

    public GraphicsCardMasterHttpClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<PaginatedResult<GraphicsCardModel>> GetAllAsync(PagedRequest pagedRequest)
    {
        var response = await _httpClient.GetAsync("api/masters/graphicsCards".ToPagedRoute(pagedRequest));
        return await response.ToPaginatedResult<GraphicsCardModel>();
    }

    public async Task<IResult<GraphicsCardModel>> GetByIdAsync(string oemSerialNo)
    {
        var response = await _httpClient.GetAsync($"api/masters/graphicsCards/{oemSerialNo}");
        return await response.ToResult<GraphicsCardModel>();
    }

    public async Task<IResult<string>> AddAsync(GraphicsCardModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/masters/graphicsCards", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> EditAsync(GraphicsCardModel model)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/masters/graphicsCards/{model.OemSerialNo}", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> DeleteAsync(string oemSerialNo)
    {
        var response = await _httpClient.DeleteAsync($"api/masters/graphicsCards/{oemSerialNo}");
        return await response.ToResult<string>();
    }
}
