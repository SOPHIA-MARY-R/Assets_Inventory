using Fluid.Shared.Models;

namespace Fluid.Core.Features;

public class SystemConfigurationService : ISystemConfigurationService
{
    private readonly IUnitOfWork _unitOfWork;

    public SystemConfigurationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SystemConfiguration> GetSystemConfiguration(string assetTag)
    {
        var sysConfig = new SystemConfiguration();
        return sysConfig;
    }
}