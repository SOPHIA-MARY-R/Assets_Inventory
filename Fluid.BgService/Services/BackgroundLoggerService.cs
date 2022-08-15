using Fluid.BgService.Models;

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
            if (DateTime.Now < _options.Value.NextLogDateTime || _systemConfigurationService.IsWriting) continue;
            await _systemConfigurationService.LogSystemConfiguration();
        }
    }
}