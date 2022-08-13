using Fluid.BgService.Models;
using Fluid.BgService.Services;
using Microsoft.Extensions.Options;

namespace Fluid.BgService.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MachineIdentifier>(configuration.GetSection(nameof(MachineIdentifier)));
        services.Configure<TechnicianCredentials>(configuration.GetSection(nameof(TechnicianCredentials)));
        return services;
    }


    public static IServiceCollection ConfigureWritable<T>(this IServiceCollection services, IConfigurationSection section, string file = "appsettings.json") where T : class, new()
    {
        services.Configure<T>(section);
        services.AddTransient(provider =>
        {
            var configuration = (IConfigurationRoot)provider.GetService<IConfiguration>();
            var environment = provider.GetService<IHostEnvironment>();
            var options = provider.GetService<IOptionsMonitor<T>>();
            return new WritableOptions<T>(environment, options, configuration, section.Key, file);
        });
        return services;
    }
}
