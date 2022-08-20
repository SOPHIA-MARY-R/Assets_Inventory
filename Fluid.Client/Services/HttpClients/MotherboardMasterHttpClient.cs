using Fluid.Client.Extensions;
using Fluid.Shared.Requests;
using Fluid.Shared.Wrapper;
using System.Net.Http.Json;
using Fluid.Shared.Entities;

namespace Fluid.Client.Services.HttpClients;

public class MotherboardMasterHttpClient
{
    private readonly HttpClient _httpClient;

    public MotherboardMasterHttpClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<PaginatedResult<MotherboardInfo>> GetAllAsync(PagedRequest pagedRequest)
    {
        return await _httpClient.GetFromJsonAsync<PaginatedResult<MotherboardInfo>>("api/masters/motherboards".ToPagedRoute(pagedRequest));
    }

    public async Task<IResult<MotherboardInfo>> GetByIdAsync(string oemSerialNo)
    {
        return await _httpClient.GetFromJsonAsync<Result<MotherboardInfo>>($"api/masters/motherboards/{oemSerialNo}");
    }

    public async Task<IResult<string>> AddAsync(MotherboardInfo model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/masters/motherboards", model);
        return await response.ToResult<string>();
    }

    public async Task<IResult<string>> EditAsync(MotherboardInfo model)
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
