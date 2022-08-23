using Fluid.Shared.Models;
using System.Management;
using System.Runtime.InteropServices;
using System.Text.Json;
using Fluid.BgService.Extensions;
using Fluid.BgService.Models;
using Fluid.Shared.Entities;
using Fluid.Shared.Enums.Technical;

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

    public SystemConfiguration SystemConfiguration { get; set; }

    public bool IsWriting { get; private set; }

    public async Task<bool> LogSystemConfiguration()
    {
        //if (SystemConfiguration is null) return false;
        IsWriting = true;
        var response = await _httpClient.PostAsJsonAsync("api/feed-log/feed", SystemConfiguration);
        var result = await response.ToResult<SystemConfiguration>();
        if (result.Succeeded)
        {
            SystemConfiguration = result.Data;
            var fileProvider = _environment.ContentRootFileProvider;
            var fileInfo = fileProvider.GetFileInfo("SystemConfiguration.json");
            var physicalPath = fileInfo.PhysicalPath;
            var json = JsonSerializer.SerializeToUtf8Bytes(result.Data, typeof(SystemConfiguration));
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

    public static MotherboardInfo GetMotherboardDetails()
    {
        var motherboard = new MotherboardInfo();
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
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

    public static IEnumerable<PhysicalMemoryInfo> GetPhysicalMemoryInfos()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");

            foreach (var obj in searcher.Get())
            {
                var physicalMemory = new PhysicalMemoryInfo();
                physicalMemory.OemSerialNo = obj["SerialNumber"].ToString()?.Trim();

                physicalMemory.Manufacturer = obj["Manufacturer"].ToString()?.Trim();

                physicalMemory.Capacity = Convert.ToInt32(Convert.ToDouble(obj["Capacity"].ToString()?.Trim()) / Math.Pow(2,30));

                physicalMemory.MemoryType = Enum.Parse<MemoryType>(int.Parse(obj["SMBiosMemoryType"].ToString()?.Trim() ?? "1").ToString());

                physicalMemory.Speed = double.Parse(obj["Speed"].ToString()?.Trim() ?? "0.00");

                physicalMemory.FormFactor = Enum.Parse<MemoryFormFactor>(int.Parse(obj["FormFactor"].ToString()?.Trim() ?? "0").ToString());

                yield return physicalMemory;
            }
        }
    }
}