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

    public async Task<bool> LogSystemConfiguration()
    {
        //if (SystemConfiguration is null) return false;
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
            return true;
        }

        _options.Update(logTime =>
        {
            logTime.LastLoggedDateTime = _options.Value.LastLoggedDateTime;
            logTime.NextLogDateTime = DateTime.Now.AddMinutes(_options.Value.RetryCoolDownMinutes);
            logTime.CoolDownMinutes = _options.Value.CoolDownMinutes;
            logTime.RetryCoolDownMinutes = _options.Value.RetryCoolDownMinutes;
        });
        return false;
    }

    public static IEnumerable<MotherboardInfo> GetMotherboardsDetails()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
            foreach (var obj in searcher.Get())
            {
                var motherboard = new MotherboardInfo
                {
                    OemSerialNo = obj["SerialNumber"].ToString(),
                    Manufacturer = obj["Manufacturer"].ToString(),
                    Model = obj["Product"].ToString()
                };

                //motherboard.Version = obj["Version"].ToString();

                //motherboard.Status = obj["Status"].ToString();
                yield return motherboard;
            }
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
        }
    }

    public static IEnumerable<PhysicalMemoryInfo> GetPhysicalMemoryInfos()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");

            foreach (var obj in searcher.Get())
            {
                var physicalMemory = new PhysicalMemoryInfo
                {
                    OemSerialNo = obj["SerialNumber"].ToString()?.Trim(),
                    Manufacturer = obj["Manufacturer"].ToString()?.Trim(),
                    Capacity = Convert.ToInt32(Convert.ToDouble(obj["Capacity"].ToString()?.Trim()) / Math.Pow(2, 30)),
                    MemoryType =
                        Enum.Parse<MemoryType>(int.Parse(obj["SMBiosMemoryType"].ToString()?.Trim() ?? "1").ToString()),
                    Speed = double.Parse(obj["Speed"].ToString()?.Trim() ?? "0.00"),
                    FormFactor =
                        Enum.Parse<MemoryFormFactor>(int.Parse(obj["FormFactor"].ToString()?.Trim() ?? "0").ToString())
                };

                yield return physicalMemory;
            }
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
        }
    }

    public static IEnumerable<HardDiskInfo> GetHardDisksInfo()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            var scope = new ManagementScope(@"\\.\root\microsoft\windows\storage");

            scope.Connect();

            var searcher = new ManagementObjectSearcher(scope, new ObjectQuery("SELECT * FROM MSFT_PhysicalDisk"));
            foreach (var obj in searcher.Get())
            {
                var hardDisk = new HardDiskInfo
                {
                    Model = obj["Model"].ToString()?.Trim(),
                    MediaType = Enum.Parse<DriveMediaType>(Convert.ToInt32(obj["MediaType"].ToString()?.Trim() ?? "0").ToString()),
                    BusType = Enum.Parse<DriveBusType>(Convert.ToInt32(obj["BusType"].ToString()?.Trim()).ToString()),
                    HealthCondition = Enum.Parse<DriveHealthCondition>(Convert.ToInt32(obj["HealthStatus"].ToString()?.Trim() ?? "0").ToString()),
                    //Caption = obj["FriendlyName"].ToString()?.Trim();
                    Size = Convert.ToInt32(Convert.ToDouble(obj["Size"]) / Math.Pow(2,30)),
                    OemSerialNo = obj.GetPropertyValue("SerialNumber").ToString()?.Trim()
                };

                yield return hardDisk;
            }
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
        }
    }
}