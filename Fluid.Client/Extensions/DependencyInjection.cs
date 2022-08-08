using Blazored.LocalStorage;
using Fluid.Client.Authentication;
using Fluid.Client.Services;
using Fluid.Client.Services.HttpClients;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace Fluid.Client.Extensions;

public static class DependencyInjection
{
    public static WebAssemblyHostBuilder AddClientServices(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddAuthentication();
        builder.Services.AddHttpClients(builder.HostEnvironment.BaseAddress);
        builder.Services.AddMudServices();
        builder.Services.AddBlazoredLocalStorage();
        return builder;
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services)
    {
        services.AddAuthorizationCore();
        services.AddTransient<AuthorizationHeaderHandler>();
        services.AddScoped<AuthenticationStateProvider, TechnicianAuthStateProvider>();
        services.AddScoped<TechnicianAuthStateProvider>();
        return services;
    }

    public static IServiceCollection AddHttpClients(this IServiceCollection services, string baseAddress)
    {
        services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Fluid.API").EnableIntercept(sp));
        services.AddHttpClient("Fluid.API", client => client.BaseAddress = new Uri(baseAddress)).AddHttpMessageHandler<AuthorizationHeaderHandler>();
        services.AddHttpClientInterceptor();
        services.AddTransient<KeyboardMasterHttpClient>();
        services.AddTransient<PhysicalMemoryMasterHttpClient>();
        services.AddTransient<MouseMasterHttpClient>();
        services.AddTransient<MotherboardMasterHttpClient>();
        services.AddTransient<HardDiskMasterHttpClient>();
        services.AddTransient<UserHttpClient>();
        services.AddTransient<HttpClientInterceptorManager>();
        return services;
    }
}
