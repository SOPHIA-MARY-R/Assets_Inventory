using Fluid.Shared.Models;
using System.Management;
using System.Runtime.InteropServices;
using System.Text.Json;
using Fluid.BgService.Extensions;
using MudBlazor;

namespace Fluid.BgService.Services;

public class SystemConfigurationService
{
    private readonly HttpClient _httpClient;
    private readonly IHostEnvironment _environment;
    private readonly ISnackbar _snackbar;

    public SystemConfigurationService(HttpClient httpClient, MachineIdentifierService machineIdentifierService, IHostEnvironment environment, ISnackbar snackbar)
    {
        _httpClient = httpClient;
        _environment = environment;
        _snackbar = snackbar;
        SystemConfiguration.MachineDetails.AssetTag = machineIdentifierService.MachineIdentifier.AssetTag;
        DeserializeSystemConfiguration();
    }

    private void DeserializeSystemConfiguration()
    {
        var fileProvider = _environment.ContentRootFileProvider;
        var fileInfo = fileProvider.GetFileInfo("SystemConfiguration.json");
        var physicalPath = fileInfo.PhysicalPath;
        var jsonContent = File.ReadAllText(physicalPath);
        var sysConfig = JsonSerializer.Deserialize<SystemConfiguration>(jsonContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = false });
        SystemConfiguration = sysConfig;
    }

    public async Task<bool> SerializeSystemConfiguration()
    {
        var response = await _httpClient.PostAsJsonAsync("api/feedlog/feed", SystemConfiguration);
        var result = await response.ToResult();
        if (result.Succeeded)
        {
            var fileProvider = _environment.ContentRootFileProvider;
            var fileInfo = fileProvider.GetFileInfo("SystemConfiguration.json");
            var physicalPath = fileInfo.PhysicalPath;
            var json = JsonSerializer.SerializeToUtf8Bytes(SystemConfiguration, typeof(SystemConfiguration));
            await File.WriteAllBytesAsync(physicalPath, json);
            return true;
        }
        else
        {
            foreach (var message in result.Messages)
            {
                _snackbar.Add(message, Severity.Error);
            }
            return false;
        }
    }

    public SystemConfiguration SystemConfiguration { get; set; } = new();

    public MotherboardModel GetMotherboardDetails()
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
        else if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {

        }
        return null;
    }
}
