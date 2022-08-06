using Fluid.Client.Services.HttpClients;

namespace Fluid.Client.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddHttpClients(this IServiceCollection services, string baseAddress)
    {
        services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });
        services.AddTransient<KeyboardMasterHttpClient>();
        services.AddTransient<MouseMasterHttpClient>();
        return services;
    }
}
