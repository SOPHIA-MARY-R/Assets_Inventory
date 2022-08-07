using Fluid.Client.Services.HttpClients;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Headers;
using Toolbelt.Blazor;

namespace Fluid.Client.Services;

public class HttpClientInterceptorManager
{
    private readonly HttpClientInterceptor _httpClientInterceptor;
    private readonly UserHttpClient _userHttpClient;
    private readonly NavigationManager _navigationManager;
    private readonly ISnackbar _snackbar;

    public HttpClientInterceptorManager(HttpClientInterceptor httpClientInterceptor, UserHttpClient userHttpClient, NavigationManager navigationManager, ISnackbar snackbar)
    {
        _httpClientInterceptor = httpClientInterceptor;
        _userHttpClient = userHttpClient;
        _navigationManager = navigationManager;
        _snackbar = snackbar;
    }

    public void RegisterEvent() => _httpClientInterceptor.BeforeSendAsync += InterceptBeforeHttpAsync;

    public async Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e)
    {
        var absPath = e.Request.RequestUri.AbsolutePath;
        if (!absPath.Contains("technician") && e.Request.Method != HttpMethod.Get)
        {
            try
            {
                var token = await _userHttpClient.TryRefreshToken();
                if (!string.IsNullOrEmpty(token))
                {
                    _snackbar.Add("Refreshed Token.", Severity.Success);
                    e.Request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _snackbar.Add("You are Logged Out.", Severity.Error);
                await _userHttpClient.Logout();
                _navigationManager.NavigateTo("/");
            }
        }
    }

    public void DisposeEvent() => _httpClientInterceptor.BeforeSendAsync -= InterceptBeforeHttpAsync;
}
