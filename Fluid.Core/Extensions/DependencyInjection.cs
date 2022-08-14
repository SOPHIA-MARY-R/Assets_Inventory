using Fluid.Core.Features;
using Fluid.Core.Features.Masters;
using Fluid.Core.Persistence;
using Fluid.Core.Repositories;
using Fluid.Shared.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fluid.Core.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration, string connectionStringKey)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString(connectionStringKey)));
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
        services.AddIdentityCore<AppUser>().AddEntityFrameworkStores<AppDbContext>();
        return services;
    }

    public static IServiceCollection AddFeatures(this IServiceCollection services)
    {
        services.AddTransient<IKeyboardMasterService, KeyboardMasterService>();
        services.AddTransient<ITechnicianUserService, TechnicianUserService>();
        services.AddTransient<IMouseMasterService, MouseMasterService>();
        services.AddTransient<IMotherboardMasterService, MotherboardMasterService>();
        services.AddTransient<IHardDiskMasterService, HardDiskMasterService>();
        services.AddTransient<IPhysicalMemoryMasterService, PhysicalMemoryMasterService>();
        services.AddTransient<IProcessorMasterService, ProcessorMasterService>();
        services.AddTransient<IGraphicsCardMasterService, GraphicsCardMasterService>();
        services.AddTransient<IMachineMasterService, MachineMasterService>();
        services.AddTransient<IMonitorMasterService, MonitorMasterService>();
        services.AddTransient<ICameraMasterService, CameraMasterService>();
        services.AddTransient<IFeedLogService, FeedLogService>();
        return services;
    }
}
