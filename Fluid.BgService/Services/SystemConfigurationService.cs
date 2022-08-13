using Fluid.Shared.Models;

namespace Fluid.BgService.Services;

public class SystemConfigurationService
{
    public SystemConfigurationService()
    {

    }

    public SystemConfiguration SystemConfiguration { get; private set; } = new();

    public void SetSystemConfiguration(SystemConfiguration systemConfiguration)
    {
        SystemConfiguration = systemConfiguration;
    }
}
