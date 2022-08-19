using Fluid.Shared.Models;

namespace Fluid.Core.Features;

public interface ISystemConfigurationService
{
    Task<SystemConfiguration> GetSystemConfiguration(string assetTag);
    Task<IResult> AddSystemConfiguration(SystemConfiguration systemConfiguration);
    Task<IResult> EditSystemConfiguration(SystemConfiguration systemConfiguration);
}