using Fluid.Client.Extensions;
using Fluid.Shared.Models;
using Fluid.Shared.Requests;
using Fluid.Shared.Wrapper;
using System.Net.Http.Json;

namespace Fluid.Client.Services.HttpClients;

public class MotherboardMasterHttpClient
{
    private readonly HttpClient _httpClient;

    public MotherboardMasterHttpClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<PaginatedResult<MotherboardModel>> GetAllAsync(PagedRequest pagedRequest)
    {
        var response = await _httpClient.GetAsync("api/masters/motherboards".ToPagedRoute(pagedRequest));
        return await response.ToPaginatedResult<MotherboardModel>();
    }

    public async Task<IResult<MotherboardModel>> GetByIdAsync(string oemSerialNo)
    {
        var response = await _httpClient.GetAsync($"api/masters/motherboards/{oemSerialNo}");
        return await response.ToResult<MotherboardModel>();
    }

    public async Task<IResult<string>> AddAsync(MotherboardModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/masters/motherboards", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> EditAsync(MotherboardModel model)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/masters/motherboards/{model.OemSerialNo}", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> DeleteAsync(string oemSerialNo)
    {
        var response = await _httpClient.DeleteAsync($"api/masters/motherboards/{oemSerialNo}");
        return await response.ToResult<string>();
    }
}
