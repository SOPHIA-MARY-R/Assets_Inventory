using Blazored.LocalStorage;
using Fluid.Client.Authentication;
using Fluid.Client.Extensions;
using Fluid.Shared.Constants;
using Fluid.Shared.Requests;
using Fluid.Shared.Responses;
using Fluid.Shared.Wrapper;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Fluid.Client.Services.HttpClients;

public class UserHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly TechnicianAuthStateProvider _authStateProvider;
    private readonly ILocalStorageService _localStorageService;

    public UserHttpClient(HttpClient httpClient, TechnicianAuthStateProvider authStateProvider, ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _authStateProvider = authStateProvider;
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
            await _localStorageService.SetItemAsync(StorageConstants.AuthToken, token);
            await _localStorageService.SetItemAsync(StorageConstants.RefreshToken, refreshToken);
            _authStateProvider.MarkUserAsAuthenticated(loginRequest.UserName);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await Result.SuccessAsync();
        }
        return await Result.FailAsync(result.Messages);
    }

    public async Task<IResult> Logout()
    {
        await _localStorageService.RemoveItemAsync(StorageConstants.AuthToken);
        await _localStorageService.RemoveItemAsync(StorageConstants.RefreshToken);
        _authStateProvider.MarkUserAsLoggedOut();
        _httpClient.DefaultRequestHeaders.Authorization = null;
        return await Result.SuccessAsync();
    }

    public async Task<string> RefreshToken()
    {
        var token = await _localStorageService.GetItemAsync<string>(StorageConstants.AuthToken);
        var refreshToken = await _localStorageService.GetItemAsync<string>(StorageConstants.RefreshToken);
        var response = await _httpClient.PostAsJsonAsync("api/technician/refresh-token", new RefreshTokenRequest { Token = token, RefreshToken = refreshToken });
        var result = await response.ToResult<LoginResponse>();
        if (!result.Succeeded)
        {
            throw new ApplicationException("Something went wrong during the refresh token action");
        }
        token = result.Data.Token;
        refreshToken = result.Data.RefreshToken;
        await _localStorageService.SetItemAsync(StorageConstants.AuthToken, token);
        await _localStorageService.SetItemAsync(StorageConstants.RefreshToken, refreshToken);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return token;
    }

    public async Task<string> TryForceRefreshToken()
    {
        var availableToken = await _localStorageService.GetItemAsync<string>(StorageConstants.RefreshToken);
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
