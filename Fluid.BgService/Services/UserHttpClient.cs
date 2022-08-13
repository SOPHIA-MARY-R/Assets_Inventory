using Blazored.LocalStorage;
using Fluid.BgService.Authentication;
using Fluid.BgService.Extensions;
using Fluid.BgService.Models;
using Fluid.Shared.Requests;
using Fluid.Shared.Responses;
using Fluid.Shared.Wrapper;
using System.Net.Http.Headers;
using IResult = Fluid.Shared.Wrapper.IResult;

namespace Fluid.BgService.Services;

public class UserHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly TechnicianAuthStateProvider _authStateProvider;
    private readonly TechnicianCredentialsService _credentialsService;
    private readonly ILocalStorageService _localStorageService;

    public UserHttpClient(HttpClient httpClient, TechnicianAuthStateProvider authStateProvider, TechnicianCredentialsService credentialsService, ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _authStateProvider = authStateProvider;
        _credentialsService = credentialsService;
        _localStorageService = localStorageService;
    }

    public async Task<IResult> Login(LoginRequest loginRequest)
    {
        var response = await _httpClient.PostAsJsonAsync("api/technician/login", loginRequest);
        var result = await response.ToResult<LoginResponse>();
        if (result.Succeeded)
        {
            var token = result.Data.Token;
            var refreshToken = result.Data.RefreshToken;
            await _localStorageService.SetItemAsync("authToken", token);
            await _localStorageService.SetItemAsync("refreshToken", refreshToken);
            _authStateProvider.MarkUserAsAuthenticated(loginRequest.UserName);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _credentialsService.UpdateTechnicianCredentials(new TechnicianCredentials { UserName = loginRequest.UserName, Password = loginRequest.Password });
            return await Result.SuccessAsync();
        }
        return await Result.FailAsync(result.Messages);
    }

    public async Task<IResult> Logout()
    {
        await _localStorageService.RemoveItemAsync("authToken");
        await _localStorageService.RemoveItemAsync("refreshToken");
        _authStateProvider.MarkUserAsLoggedOut();
        _httpClient.DefaultRequestHeaders.Authorization = null;
        return await Result.SuccessAsync();
    }

    public async Task<string> RefreshToken()
    {
        var token = await _localStorageService.GetItemAsync<string>("authToken");
        var refreshToken = await _localStorageService.GetItemAsync<string>("refreshToken");
        var response = await _httpClient.PostAsJsonAsync("api/technician/refresh-token", new RefreshTokenRequest { Token = token, RefreshToken = refreshToken });
        var result = await response.ToResult<LoginResponse>();
        if (!result.Succeeded)
        {
            throw new ApplicationException("Something went wrong during the refresh token action");
        }
        token = result.Data.Token;
        refreshToken = result.Data.RefreshToken;
        await _localStorageService.SetItemAsync("authToken", token);
        await _localStorageService.SetItemAsync("refreshToken", refreshToken);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return token;
    }

    public async Task<string> TryForceRefreshToken()
    {
        var availableToken = await _localStorageService.GetItemAsync<string>("refreshToken");
        if (string.IsNullOrEmpty(availableToken)) return string.Empty;
        var authState = await _authStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        var exp = user.FindFirst(c => c.Type.Equals("exp"))?.Value;
        var expTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(exp));
        var diff = expTime - DateTime.Now;
        if (diff.TotalMinutes <= 1)
            return await RefreshToken();
        return string.Empty;
    }

    public async Task<string> TryRefreshToken()
    {
        return await RefreshToken();
    }
}
