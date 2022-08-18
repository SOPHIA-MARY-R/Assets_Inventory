using Fluid.Shared.Models;

namespace Fluid.Core.Features;

public interface ISystemConfigurationService
{
    Task<SystemConfiguration> GetSystemConfiguration(string assetTag);
}