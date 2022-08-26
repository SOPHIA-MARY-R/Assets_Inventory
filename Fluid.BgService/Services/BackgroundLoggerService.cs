using System.Text.Json;
using Fluid.BgService.Models;
using Fluid.Shared.Models;

namespace Fluid.BgService.Services;

public class BackgroundLoggerService : BackgroundService
{
    private readonly WritableOptions<BackgroundLogTime> _options;
    private readonly SystemConfigurationService _systemConfigurationService;

    public BackgroundLoggerService(WritableOptions<BackgroundLogTime> options, SystemConfigurationService systemConfigurationService)
    {
        _options = options;
        _systemConfigurationService = systemConfigurationService;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var jsonRaw = JsonSerializer.Serialize(_systemConfigurationService.SystemConfiguration,
                typeof(SystemConfiguration));
            var previousSysConfig = JsonSerializer.Deserialize(jsonRaw, typeof(SystemConfiguration));
            _systemConfigurationService.SystemConfiguration.Motherboards = SystemConfigurationService.GetMotherboardsDetails().ToList();
            _systemConfigurationService.SystemConfiguration.PhysicalMemories = SystemConfigurationService.GetPhysicalMemoryInfos().ToList();
            _systemConfigurationService.SystemConfiguration.HardDisks = SystemConfigurationService.GetHardDisksInfo().ToList();
            if (DateTime.Now < _options.Value.NextLogDateTime || previousSysConfig != _systemConfigurationService.SystemConfiguration) 
                await Task.Delay(_options.Value.NextLogDateTime - DateTime.Now, stoppingToken);
            await _systemConfigurationService.LogSystemConfiguration();
        }
    }
}