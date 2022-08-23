using System.Net.Http.Json;
using Fluid.Client.Extensions;
using Fluid.Shared.Entities;
using Fluid.Shared.Models.FilterModels;
using Fluid.Shared.Wrapper;

namespace Fluid.Client.Services.HttpClients;

public class HardwareChangeLogsHttpClient
{
    private readonly HttpClient _httpClient;

    public HardwareChangeLogsHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PaginatedResult<HardwareChangeLog>> GetAllAsync(HardwareChangeLogFilter filter)
    {
        var result = await _httpClient.PostAsJsonAsync("api/hardware-changelogs/get-all", filter);
        return await result.ToPaginatedResult<HardwareChangeLog>();
    }
}