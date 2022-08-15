using Fluid.Shared.Models;
using System.Management;
using System.Runtime.InteropServices;
using System.Text.Json;
using Fluid.BgService.Extensions;
using Fluid.BgService.Models;

namespace Fluid.BgService.Services;

public class SystemConfigurationService
{
    private readonly WritableOptions<BackgroundLogTime> _options;
    private readonly IHostEnvironment _environment;
    private readonly HttpClient _httpClient;

    public SystemConfigurationService(WritableOptions<BackgroundLogTime> options, IHostEnvironment environment,
        MachineIdentifierService machineIdentifierService)
    {
        _options = options;
        _environment = environment;
        _httpClient = new HttpClient()
            { BaseAddress = new Uri(machineIdentifierService.MachineIdentifier.ServerAddress) };
        DeserializeSystemConfiguration();
    }

    private void DeserializeSystemConfiguration()
    {
        var fileProvider = _environment.ContentRootFileProvider;
        var fileInfo = fileProvider.GetFileInfo("SystemConfiguration.json");
        var physicalPath = fileInfo.PhysicalPath;
        var jsonContent = File.ReadAllText(physicalPath);
        var sysConfig = JsonSerializer.Deserialize<SystemConfiguration>(jsonContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = false });
        SystemConfiguration = sysConfig;
    }

    public SystemConfiguration SystemConfiguration { get; set; } = new();

    public bool IsWriting { get; private set; }

    public async Task<bool> LogSystemConfiguration()
    {
        IsWriting = true;
        var response = await _httpClient.PostAsJsonAsync("api/feed-log/feed", SystemConfiguration);
        var result = await response.ToResult();
        if (result.Succeeded)
        {
            var fileProvider = _environment.ContentRootFileProvider;
            var fileInfo = fileProvider.GetFileInfo("SystemConfiguration.json");
            var physicalPath = fileInfo.PhysicalPath;
            var json = JsonSerializer.SerializeToUtf8Bytes(SystemConfiguration, typeof(SystemConfiguration));
            await File.WriteAllBytesAsync(physicalPath, json);
            _options.Update(logTime =>
            {
                logTime.LastLoggedDateTime = DateTime.Now;
                logTime.NextLogDateTime = DateTime.Now.AddMinutes(_options.Value.CoolDownMinutes);
                logTime.CoolDownMinutes = _options.Value.CoolDownMinutes;
                logTime.RetryCoolDownMinutes = _options.Value.RetryCoolDownMinutes;
            });
            IsWriting = false;
            return true;
        }
        else
        {
            _options.Update(logTime =>
            {
                logTime.LastLoggedDateTime = _options.Value.LastLoggedDateTime;
                logTime.NextLogDateTime = DateTime.Now.AddMinutes(_options.Value.RetryCoolDownMinutes);
                logTime.CoolDownMinutes = _options.Value.CoolDownMinutes;
                logTime.RetryCoolDownMinutes = _options.Value.RetryCoolDownMinutes;
            });
            IsWriting = false;
            return false;
        }
    }

    public static MotherboardModel GetMotherboardDetails()
    {
        var motherboard = new MotherboardModel();
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            var searcher = new ManagementObjectSearcher("Select * From Win32_BaseBoard");
            foreach (var obj in searcher.Get())
            {
                motherboard.OemSerialNo = obj["SerialNumber"].ToString();

                motherboard.Manufacturer = obj["Manufacturer"].ToString();

                motherboard.Model = obj["Product"].ToString();

                //motherboard.Version = obj["Version"].ToString();

                //motherboard.Status = obj["Status"].ToString();
                return motherboard;
            }
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
        }

        return null;
    }
}