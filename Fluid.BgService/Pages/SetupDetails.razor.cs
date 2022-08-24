using Fluid.BgService.Dialogs;
using Fluid.BgService.Services;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;
using MudBlazor;

namespace Fluid.BgService.Pages;

public partial class SetupDetails
{
    private SystemConfiguration Model { get; set; } = new();

    protected override Task OnInitializedAsync()
    {
        Model = systemConfigurationService.SystemConfiguration ?? new SystemConfiguration();
        Model.MachineDetails.AssetTag = machineIdentifierService.MachineIdentifier.AssetTag;
        return base.OnInitializedAsync();
    }

    private async void Submit()
    {
        Model.Motherboards.ForEach(x => x.MachineId = Model.MachineDetails.AssetTag);
        Model.PhysicalMemories.ForEach(x => x.MachineId = Model.MachineDetails.AssetTag);
        Model.Processors.ForEach(x => x.MachineId = Model.MachineDetails.AssetTag);
        Model.HardDisks.ForEach(x => x.MachineId = Model.MachineDetails.AssetTag);
        Model.Keyboards.ForEach(x => x.MachineId = Model.MachineDetails.AssetTag);
        Model.Monitors.ForEach(x => x.MachineId = Model.MachineDetails.AssetTag);
        Model.Mouses.ForEach(x => x.MachineId = Model.MachineDetails.AssetTag);
        systemConfigurationService.SystemConfiguration = Model;
        var isSucceeded = await systemConfigurationService.LogSystemConfiguration();
        if (isSucceeded)
        {
            navigationManager.NavigateTo("/");
        }
        else
        {
            Console.WriteLine("SystemConfiguration upload failed");
        }
    }

    private async Task InvokeMotherboardDialog(MotherboardInfo info)
    {
        var parameters = new DialogParameters
        {
            { nameof(AddEditMachineMotherboardDialog.Model), info }
        };
        var options = new DialogOptions { CloseButton = true, FullWidth = true, DisableBackdropClick = true, Position = DialogPosition.TopCenter };
        var dialog = dialogService.Show<AddEditMachineMotherboardDialog>("", parameters, options);
        var result = await dialog.Result;
        if (result.Cancelled) return;
        var updatedInfo = result.Data as MotherboardInfo;
        var oemSerialNo = updatedInfo?.OemSerialNo.Trim();
        if (Model.Motherboards.Any(x => x.OemSerialNo.Trim() == oemSerialNo))
            Model.Motherboards.Remove(Model.Motherboards.First(x => x.OemSerialNo.Trim() == oemSerialNo));
        Model.Motherboards.Add(updatedInfo);
    }

    private async Task InvokePhysicalMemoryDialog(PhysicalMemoryInfo info)
    {
        var parameters = new DialogParameters
        {
            { nameof(AddEditMachinePhysicalMemoryDialog.Model), info }
        };
        var options = new DialogOptions { CloseButton = true, FullWidth = true, DisableBackdropClick = true, Position = DialogPosition.TopCenter };
        var dialog = dialogService.Show<AddEditMachinePhysicalMemoryDialog>("", parameters, options);
        var result = await dialog.Result;
        if (result.Cancelled) return;
        var updatedInfo = result.Data as PhysicalMemoryInfo;
        var oemSerialNo = updatedInfo?.OemSerialNo.Trim();
        if (Model.PhysicalMemories.Any(x => x.OemSerialNo.Trim() == oemSerialNo))
            Model.PhysicalMemories.Remove(Model.PhysicalMemories.First(x => x.OemSerialNo.Trim() == oemSerialNo));
        Model.PhysicalMemories.Add(updatedInfo);
    }

    private async Task InvokeKeyboardDialog(KeyboardInfo info)
    {
        var parameters = new DialogParameters
        {
            { nameof(AddEditMachineKeyboardDialog.Model), info }
        };
        var options = new DialogOptions { CloseButton = true, FullWidth = true, DisableBackdropClick = true, Position = DialogPosition.TopCenter };
        var dialog = dialogService.Show<AddEditMachineKeyboardDialog>("", parameters, options);
        var result = await dialog.Result;
        if (result.Cancelled) return;
        var updatedInfo = result.Data as KeyboardInfo;
        var oemSerialNo = updatedInfo?.OemSerialNo.Trim();
        if (Model.Keyboards.Any(x => x.OemSerialNo.Trim() == oemSerialNo))
            Model.Keyboards.Remove(Model.Keyboards.First(x => x.OemSerialNo.Trim() == oemSerialNo));
        Model.Keyboards.Add(updatedInfo);
    }
    
    private async Task InvokeMonitorDialog(MonitorInfo info)
    {
        var parameters = new DialogParameters
        {
            { nameof(AddEditMachineMonitorDialog.Model), info }
        };
        var options = new DialogOptions { CloseButton = true, FullWidth = true, DisableBackdropClick = true, Position = DialogPosition.TopCenter };
        var dialog = dialogService.Show<AddEditMachineMonitorDialog>("", parameters, options);
        var result = await dialog.Result;
        if (result.Cancelled) return;
        var updatedInfo = result.Data as MonitorInfo;
        var oemSerialNo = updatedInfo?.OemSerialNo.Trim();
        if (Model.Monitors.Any(x => x.OemSerialNo.Trim() == oemSerialNo))
            Model.Monitors.Remove(Model.Monitors.First(x => x.OemSerialNo.Trim() == oemSerialNo));
        Model.Monitors.Add(updatedInfo);
    }
    
    private async Task InvokeMouseDialog(MouseInfo info)
    {
        var parameters = new DialogParameters
        {
            { nameof(AddEditMachineMouseDialog.Model), info }
        };
        var options = new DialogOptions { CloseButton = true, FullWidth = true, DisableBackdropClick = true, Position = DialogPosition.TopCenter };
        var dialog = dialogService.Show<AddEditMachineMouseDialog>("", parameters, options);
        var result = await dialog.Result;
        if (result.Cancelled) return;
        var updatedInfo = result.Data as MouseInfo;
        var oemSerialNo = updatedInfo?.OemSerialNo.Trim();
        if (Model.Mouses.Any(x => x.OemSerialNo.Trim() == oemSerialNo))
            Model.Mouses.Remove(Model.Mouses.First(x => x.OemSerialNo.Trim() == oemSerialNo));
        Model.Mouses.Add(updatedInfo);
    }
    
    private void DeleteMotherboardInfo(MotherboardInfo motherboardInfo)
    {
        Model.Motherboards.Remove(motherboardInfo);
    }
    
    private void DeletePhysicalMemoryInfo(PhysicalMemoryInfo physicalMemoryInfo)
    {
        Model.PhysicalMemories.Remove(physicalMemoryInfo);
    }
    
    private void DeleteHardDiskInfo(HardDiskInfo hardDiskInfo)
    {
        Model.HardDisks.Remove(hardDiskInfo);
    }
    
    private void DeleteKeyboardInfo(KeyboardInfo keyboardInfo)
    {
        Model.Keyboards.Remove(keyboardInfo);
    }
    
    private void DeleteMonitorInfo(MonitorInfo monitorInfo)
    {
        Model.Monitors.Remove(monitorInfo);
    }
    
    private void DeleteMouseInfo(MouseInfo mouseInfo)
    {
        Model.Mouses.Remove(mouseInfo);
    }

    private void AutoFill()
    {
        Model.Motherboards = SystemConfigurationService.GetMotherboardsDetails().ToList();
        Model.HardDisks = SystemConfigurationService.GetHardDisksInfo().ToList();
        Model.PhysicalMemories = SystemConfigurationService.GetPhysicalMemoryInfos().ToList();
    }
}
