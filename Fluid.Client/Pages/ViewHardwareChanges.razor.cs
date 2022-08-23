using System.Text.Json;
using Fluid.Shared.Entities;
using Fluid.Shared.Enums;
using Fluid.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Fluid.Client.Pages;

public partial class ViewHardwareChanges
{
    [Parameter]
    public string Id { get; set; }

    private SystemConfiguration SysConfigFromFeedLog { get; set; } = new SystemConfiguration();

    private SystemConfiguration SysConfigFromMaster = new SystemConfiguration();
    
    private SystemConfiguration Model = new SystemConfiguration();
    
    private bool _isNewMachine; 

    protected override async Task OnInitializedAsync()
    {
        var result = await FeedLogHttpClient.GetByIdAsync(Id);
        if (result.Succeeded)
        {
            var feedLog = result.Data;
            SysConfigFromFeedLog = (SystemConfiguration)JsonSerializer.Deserialize(feedLog.JsonRaw, typeof(SystemConfiguration));
            var assetTag = SysConfigFromFeedLog?.MachineDetails.AssetTag;
            var sysConfigResult = await MachineMasterHttpClient.GetByIdAsync(assetTag);
            if (sysConfigResult.Succeeded)
            {
                SysConfigFromMaster = sysConfigResult.Data;
                Model.MachineDetails = SysConfigFromFeedLog.MachineDetails;
                await FillModelMotherboards();
                await FillModelPhysicalMemories();
                await FillModelHardDisks();
                await FillModelKeyboards();
                await FillModelMouses();
                await FillModelMonitors();
                //Model = SysConfigFromFeedLog; //Filling Model needs some sort of HashSet like functionality
            }
            else
            {
                _isNewMachine = true;
                await AddNewMachineNotifier();
            }
        }
        else
        {
            foreach (var message in result.Messages)
            {
                snackbar.Add(message, Severity.Error);
            }
            navigationManager.NavigateTo("/Hardware-Logs");
        }
        await base.OnInitializedAsync();
    }

    private async Task AddNewMachineNotifier()
    {
        if (await dialogService.ShowMessageBox("New Machine found",
                "Would you like to add this machine into the master? This will redirect you to add machine page.",
                "Yes", "No") == true)
        {
            navigationManager.NavigateTo($"Machine-Master/{Id}/Add");
        }
    }

    private async Task FillModelMotherboards()
    {
        Model.Motherboards = new List<MotherboardInfo>();
        foreach (var motherboard in SysConfigFromFeedLog.Motherboards)
        {
            if (SysConfigFromMaster.Motherboards.Select(x => x.OemSerialNo).Contains(motherboard.OemSerialNo))
            {
                motherboard.HardwareChangeMode = HardwareChangeMode.Unchanged;
            }
            else
            {
                var foundInDb = await MotherboardMasterHttpClient.GetByIdAsync(motherboard.OemSerialNo);
                motherboard.HardwareChangeMode = foundInDb.Succeeded ? HardwareChangeMode.AddedExisting : HardwareChangeMode.AddedNew;
            }
            Model.Motherboards.Add(motherboard);
        }
        foreach (var motherboard in SysConfigFromMaster.Motherboards.Where(motherboard => !SysConfigFromFeedLog.Motherboards.Select(x => x.OemSerialNo).Contains(motherboard.OemSerialNo)))
        {
            motherboard.HardwareChangeMode = HardwareChangeMode.Deleted;
            Model.Motherboards.Add(motherboard);
        }
    }

    private async Task FillModelPhysicalMemories()
    {
        Model.PhysicalMemories = new List<PhysicalMemoryInfo>();
        foreach (var physicalMemory in SysConfigFromFeedLog.PhysicalMemories)
        {
            if (SysConfigFromMaster.PhysicalMemories.Select(x => x.OemSerialNo).Contains(physicalMemory.OemSerialNo))
            {
                physicalMemory.HardwareChangeMode = HardwareChangeMode.Unchanged;
            }
            else
            {
                var foundInDb = await PhysicalMemoryMasterHttpClient.GetByIdAsync(physicalMemory.OemSerialNo);
                physicalMemory.HardwareChangeMode = foundInDb.Succeeded ? HardwareChangeMode.AddedExisting : HardwareChangeMode.AddedNew;
            }
            Model.PhysicalMemories.Add(physicalMemory);
        }
        foreach (var physicalMemory in SysConfigFromMaster.PhysicalMemories.Where(physicalMemory => !SysConfigFromFeedLog.PhysicalMemories.Select(x => x.OemSerialNo).Contains(physicalMemory.OemSerialNo)))
        {
            physicalMemory.HardwareChangeMode = HardwareChangeMode.Deleted;
            Model.PhysicalMemories.Add(physicalMemory);
        }
    }

    private async Task FillModelHardDisks()
    {
        Model.HardDisks = new List<HardDiskInfo>();
        foreach (var hardDiskInfo in SysConfigFromFeedLog.HardDisks)
        {
            if (SysConfigFromMaster.HardDisks.Select(x => x.OemSerialNo).Contains(hardDiskInfo.OemSerialNo))
            {
                hardDiskInfo.HardwareChangeMode = HardwareChangeMode.Unchanged;
            }
            else
            {
                var foundInDb = await PhysicalMemoryMasterHttpClient.GetByIdAsync(hardDiskInfo.OemSerialNo);
                hardDiskInfo.HardwareChangeMode = foundInDb.Succeeded ? HardwareChangeMode.AddedExisting : HardwareChangeMode.AddedNew;
            }
            Model.HardDisks.Add(hardDiskInfo);
        }
        foreach (var hardDisk in SysConfigFromMaster.HardDisks.Where(hardDisk => !SysConfigFromFeedLog.HardDisks.Select(x => x.OemSerialNo).Contains(hardDisk.OemSerialNo)))
        {
            hardDisk.HardwareChangeMode = HardwareChangeMode.Deleted;
            Model.HardDisks.Add(hardDisk);
        }
    }

    private async Task FillModelKeyboards()
    {
        Model.Keyboards = new List<KeyboardInfo>();
        foreach (var keyboard in SysConfigFromFeedLog.Keyboards)
        {
            if (SysConfigFromMaster.Keyboards.Select(x => x.OemSerialNo).Contains(keyboard.OemSerialNo))
            {
                keyboard.HardwareChangeMode = HardwareChangeMode.Unchanged;
            }
            else
            {
                var foundInDb = await KeyboardMasterHttpClient.GetByIdAsync(keyboard.OemSerialNo);
                keyboard.HardwareChangeMode = foundInDb.Succeeded ? HardwareChangeMode.AddedExisting : HardwareChangeMode.AddedNew;
            }
            Model.Keyboards.Add(keyboard);
        }
        foreach (var keyboard in SysConfigFromMaster.Keyboards.Where(keyboard => !SysConfigFromFeedLog.Keyboards.Select(x => x.OemSerialNo).Contains(keyboard.OemSerialNo)))
        {
            keyboard.HardwareChangeMode = HardwareChangeMode.Deleted;
            Model.Keyboards.Add(keyboard);
        }
    }

    private async Task FillModelMouses()
    {
        Model.Mouses = new List<MouseInfo>();
        foreach (var mouse in SysConfigFromFeedLog.Mouses)
        {
            if (SysConfigFromMaster.Mouses.Select(x => x.OemSerialNo).Contains(mouse.OemSerialNo))
            {
                mouse.HardwareChangeMode = HardwareChangeMode.Unchanged;
            }
            else
            {
                var foundInDb = await MouseMasterHttpClient.GetByIdAsync(mouse.OemSerialNo);
                mouse.HardwareChangeMode = foundInDb.Succeeded ? HardwareChangeMode.AddedExisting : HardwareChangeMode.AddedNew;
            }
            Model.Mouses.Add(mouse);
        }
        foreach (var mouse in SysConfigFromMaster.Mouses.Where(mouse => !SysConfigFromFeedLog.Mouses.Select(x => x.OemSerialNo).Contains(mouse.OemSerialNo)))
        {
            mouse.HardwareChangeMode = HardwareChangeMode.Deleted;
            Model.Mouses.Add(mouse);
        }
    }

    private async Task FillModelMonitors()
    {
        Model.Monitors = new List<MonitorInfo>();
        foreach (var monitor in SysConfigFromFeedLog.Monitors)
        {
            if (SysConfigFromMaster.Monitors.Select(x => x.OemSerialNo).Contains(monitor.OemSerialNo))
            {
                monitor.HardwareChangeMode = HardwareChangeMode.Unchanged;
            }
            else
            {
                var foundInDb = await MonitorMasterHttpClient.GetByIdAsync(monitor.OemSerialNo);
                monitor.HardwareChangeMode = foundInDb.Succeeded ? HardwareChangeMode.AddedExisting : HardwareChangeMode.AddedNew;
            }
            Model.Monitors.Add(monitor);
        }
        foreach (var monitor in SysConfigFromMaster.Monitors.Where(monitor => !SysConfigFromFeedLog.Monitors.Select(x => x.OemSerialNo).Contains(monitor.OemSerialNo)))
        {
            monitor.HardwareChangeMode = HardwareChangeMode.Deleted;
            Model.Monitors.Add(monitor);
        }
    }

    private async Task SubmitAsync()
    {
        if (_isNewMachine)
        {
            await AddNewMachineNotifier();
        }
        else
        {
            
        }
    }
}