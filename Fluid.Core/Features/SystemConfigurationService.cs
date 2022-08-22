using Fluid.Core.Extensions;
using Fluid.Core.Specifications;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features;

public class SystemConfigurationService : ISystemConfigurationService
{
    private readonly IUnitOfWork _unitOfWork;

    public SystemConfigurationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<SystemConfiguration>> GetSystemConfiguration(string assetTag)
    {
        try
        {
            if (await _unitOfWork.GetRepository<MachineInfo>().GetByIdAsync(assetTag) is null)
                throw new Exception($"System with Asset Tag {assetTag} is not found!");
            var sysConfig = new SystemConfiguration
            {
                MachineDetails = await _unitOfWork.GetRepository<MachineInfo>().GetByIdAsync(assetTag),
                Motherboards = await _unitOfWork.GetRepository<MotherboardInfo>().Entities
                    .Where(x => x.MachineId == assetTag)
                    .ToListAsync(),
                PhysicalMemories = await _unitOfWork.GetRepository<PhysicalMemoryInfo>().Entities
                    .Where(x => x.MachineId == assetTag)
                    .ToListAsync(),
                HardDisks = await _unitOfWork.GetRepository<HardDiskInfo>().Entities
                    .Where(x => x.MachineId == assetTag)
                    .ToListAsync(),
                Processors = await _unitOfWork.GetRepository<ProcessorInfo>().Entities
                    .Where(x => x.MachineId == assetTag)
                    .ToListAsync(),
                Mouses = await _unitOfWork.GetRepository<MouseInfo>().Entities
                    .Where(x => x.MachineId == assetTag)
                    .ToListAsync(),
                Keyboards = await _unitOfWork.GetRepository<KeyboardInfo>().Entities
                    .Where(x => x.MachineId == assetTag)
                    .ToListAsync(),
                Monitors = await _unitOfWork.GetRepository<MonitorInfo>().Entities
                    .Where(x => x.MachineId == assetTag)
                    .ToListAsync()
            };
            return await Result<SystemConfiguration>.SuccessAsync(sysConfig);
        }
        catch (Exception e)
        {
            return await Result<SystemConfiguration>.FailAsync(e.Message);
        }
    }

    public async Task<IResult> AddSystemConfiguration(SystemConfiguration systemConfiguration)
    {
        try
        {
            var assetTag = systemConfiguration.MachineDetails.AssetTag;
            if (await _unitOfWork.GetRepository<MachineInfo>()
                    .GetByIdAsync(assetTag) is not null)
                throw new Exception("Machine with the same asset tag already present");
            await _unitOfWork.GetRepository<MachineInfo>().AddAsync(systemConfiguration.MachineDetails);

            foreach (var motherboard in systemConfiguration.Motherboards)
            {
                var oemSerialNo = motherboard.OemSerialNo;
                if (await _unitOfWork.GetRepository<MotherboardInfo>().GetByIdAsync(oemSerialNo) is null)
                {
                    motherboard.MachineId = assetTag;
                    await _unitOfWork.GetRepository<MotherboardInfo>().AddAsync(motherboard);
                }
                else
                {
                    if (!string.IsNullOrEmpty(motherboard.MachineId))
                        throw new Exception("The Selected Motherboard is already in use by another machine");
                    motherboard.MachineId = assetTag;
                    await _unitOfWork.GetRepository<MotherboardInfo>().UpdateAsync(motherboard, oemSerialNo);
                }
            }
            
            foreach (var physicalMemory in systemConfiguration.PhysicalMemories)
            {
                var oemSerialNo = physicalMemory.OemSerialNo;
                if (await _unitOfWork.GetRepository<PhysicalMemoryInfo>().GetByIdAsync(oemSerialNo) is null)
                {
                    physicalMemory.MachineId = assetTag;
                    await _unitOfWork.GetRepository<PhysicalMemoryInfo>().AddAsync(physicalMemory);
                }
                else
                {
                    if (!string.IsNullOrEmpty(physicalMemory.MachineId))
                        throw new Exception("The Selected Physical Memory is already in use by another machine");
                    physicalMemory.MachineId = assetTag;
                    await _unitOfWork.GetRepository<PhysicalMemoryInfo>().UpdateAsync(physicalMemory, oemSerialNo);
                }
            }
            
            foreach (var hardDisk in systemConfiguration.HardDisks)
            {
                var oemSerialNo = hardDisk.OemSerialNo;
                if (await _unitOfWork.GetRepository<HardDiskInfo>().GetByIdAsync(oemSerialNo) is null)
                {
                    hardDisk.MachineId = assetTag;
                    await _unitOfWork.GetRepository<HardDiskInfo>().AddAsync(hardDisk);
                }
                else
                {
                    if (!string.IsNullOrEmpty(hardDisk.MachineId))
                        throw new Exception("The Selected Hard Disk is already in use by another machine");
                    hardDisk.MachineId = assetTag;
                    await _unitOfWork.GetRepository<HardDiskInfo>().UpdateAsync(hardDisk, oemSerialNo);
                }
            }

            foreach (var keyboard in systemConfiguration.Keyboards)
            {
                var oemSerialNo = keyboard.OemSerialNo;
                if (await _unitOfWork.GetRepository<KeyboardInfo>().GetByIdAsync(oemSerialNo) is null)
                {
                    keyboard.MachineId = assetTag;
                    await _unitOfWork.GetRepository<KeyboardInfo>().AddAsync(keyboard);
                }
                else
                {
                    if (!string.IsNullOrEmpty(keyboard.MachineId))
                        throw new Exception("The Selected Keyboard is already in use by another machine");
                    keyboard.MachineId = assetTag;
                    await _unitOfWork.GetRepository<KeyboardInfo>().UpdateAsync(keyboard, oemSerialNo);
                }
            }

            foreach (var monitor in systemConfiguration.Monitors)
            {
                var oemSerialNo = monitor.OemSerialNo;
                if (await _unitOfWork.GetRepository<MonitorInfo>().GetByIdAsync(oemSerialNo) is null)
                {
                    monitor.MachineId = assetTag;
                    await _unitOfWork.GetRepository<MonitorInfo>().AddAsync(monitor);
                }
                else
                {
                    if (!string.IsNullOrEmpty(monitor.MachineId))
                        throw new Exception("The Selected Monitor is already in use by another machine");
                    monitor.MachineId = assetTag;
                    await _unitOfWork.GetRepository<MonitorInfo>().UpdateAsync(monitor, oemSerialNo);
                }
            }

            foreach (var mouse in systemConfiguration.Mouses)
            {
                var oemSerialNo = mouse.OemSerialNo;
                if (await _unitOfWork.GetRepository<MouseInfo>().GetByIdAsync(oemSerialNo) is null)
                {
                    mouse.MachineId = assetTag;
                    await _unitOfWork.GetRepository<MouseInfo>().AddAsync(mouse);
                }
                else
                {
                    if (!string.IsNullOrEmpty(mouse.MachineId))
                        throw new Exception("The Selected Mouse is already in use by another machine");
                    mouse.MachineId = assetTag;
                    await _unitOfWork.GetRepository<MouseInfo>().UpdateAsync(mouse, oemSerialNo);
                }
            }
            systemConfiguration.MachineDetails.UpdateChangeOnClient = true;

            await _unitOfWork.Commit();
            return await Result.SuccessAsync("Machine added successfully");
        }
        catch (Exception e)
        {
            await _unitOfWork.Rollback();
            return await Result.FailAsync(e.Message);
        }
    }

    public async Task<IResult> EditSystemConfiguration(SystemConfiguration systemConfiguration, string assetTag)
    {
        try
        {
            if (await _unitOfWork.GetRepository<MachineInfo>().GetByIdAsync(assetTag) is null)
                throw new Exception("Machine does not exist in database");
            await _unitOfWork.GetRepository<MachineInfo>().UpdateAsync(systemConfiguration.MachineDetails, assetTag);

            var previousMotherboards = await _unitOfWork.GetRepository<MotherboardInfo>().Entities
                .Specify(new MotherboardInfoAssetTagSpecification(assetTag))
                .ToListAsync();
            foreach (var previousMotherboard in previousMotherboards)
                previousMotherboard.MachineId = null;
            foreach (var motherboard in systemConfiguration.Motherboards)
            {
                var oemSerialNo = motherboard.OemSerialNo;
                motherboard.MachineId = assetTag;
                if (await _unitOfWork.GetRepository<MotherboardInfo>().GetByIdAsync(motherboard.OemSerialNo) is null)
                    await _unitOfWork.GetRepository<MotherboardInfo>().AddAsync(motherboard);
                else
                    await _unitOfWork.GetRepository<MotherboardInfo>().UpdateAsync(motherboard, oemSerialNo);
            }
            
            var previousPhysicalMemories = await _unitOfWork.GetRepository<PhysicalMemoryInfo>().Entities
                .Specify(new PhysicalMemoryInfoAssetTagSpecification(assetTag))
                .ToListAsync();
            foreach (var physicalMemoryInfo in previousPhysicalMemories)
                physicalMemoryInfo.MachineId = null;
            foreach (var physicalMemory in systemConfiguration.PhysicalMemories)
            {
                var oemSerialNo = physicalMemory.OemSerialNo;
                physicalMemory.MachineId = assetTag;
                if (await _unitOfWork.GetRepository<PhysicalMemoryInfo>().GetByIdAsync(physicalMemory.OemSerialNo) is null)
                    await _unitOfWork.GetRepository<PhysicalMemoryInfo>().AddAsync(physicalMemory);
                else
                    await _unitOfWork.GetRepository<PhysicalMemoryInfo>().UpdateAsync(physicalMemory, oemSerialNo);
            }
            
            var previousHardDisks = await _unitOfWork.GetRepository<HardDiskInfo>().Entities
                .Specify(new HardDiskInfoAssetTagSpecification(assetTag))
                .ToListAsync();
            foreach (var hardDisk in previousHardDisks)
                hardDisk.MachineId = null;
            foreach (var hardDisk in systemConfiguration.HardDisks)
            {
                var oemSerialNo = hardDisk.OemSerialNo;
                hardDisk.MachineId = assetTag;
                if (await _unitOfWork.GetRepository<HardDiskInfo>().GetByIdAsync(hardDisk.OemSerialNo) is null)
                    await _unitOfWork.GetRepository<HardDiskInfo>().AddAsync(hardDisk);
                else
                    await _unitOfWork.GetRepository<HardDiskInfo>().UpdateAsync(hardDisk, oemSerialNo);
            }
            
            var previousKeyboards = await _unitOfWork.GetRepository<KeyboardInfo>().Entities
                .Specify(new KeyboardInfoAssetTagSpecification(assetTag))
                .ToListAsync();
            foreach (var previousKeyboard in previousKeyboards)
                previousKeyboard.MachineId = null;
            foreach (var keyboard in systemConfiguration.Keyboards)
            {
                var oemSerialNo = keyboard.OemSerialNo;
                keyboard.MachineId = assetTag;
                if (await _unitOfWork.GetRepository<KeyboardInfo>().GetByIdAsync(keyboard.OemSerialNo) is null)
                    await _unitOfWork.GetRepository<KeyboardInfo>().AddAsync(keyboard);
                else
                    await _unitOfWork.GetRepository<KeyboardInfo>().UpdateAsync(keyboard, oemSerialNo);
            }
            
            var previousMonitors = await _unitOfWork.GetRepository<MonitorInfo>().Entities
                .Specify(new MonitorInfoAssetTagSpecification(assetTag))
                .ToListAsync();
            foreach (var previousMonitor in previousMonitors)
                previousMonitor.MachineId = null;
            foreach (var monitor in systemConfiguration.Monitors)
            {
                var oemSerialNo = monitor.OemSerialNo;
                monitor.MachineId = assetTag;
                if (await _unitOfWork.GetRepository<MonitorInfo>().GetByIdAsync(monitor.OemSerialNo) is null)
                    await _unitOfWork.GetRepository<MonitorInfo>().AddAsync(monitor);
                else
                    await _unitOfWork.GetRepository<MonitorInfo>().UpdateAsync(monitor, oemSerialNo);
            }
            
            var previousMouses = await _unitOfWork.GetRepository<MouseInfo>().Entities
                .Specify(new MouseInfoAssetTagSpecification(assetTag))
                .ToListAsync();
            foreach (var previousMouse in previousMouses)
                previousMouse.MachineId = null;
            foreach (var mouse in systemConfiguration.Mouses)
            {
                var oemSerialNo = mouse.OemSerialNo;
                mouse.MachineId = assetTag;
                if (await _unitOfWork.GetRepository<MouseInfo>().GetByIdAsync(mouse.OemSerialNo) is null)
                    await _unitOfWork.GetRepository<MouseInfo>().AddAsync(mouse);
                else
                    await _unitOfWork.GetRepository<MouseInfo>().UpdateAsync(mouse, oemSerialNo);
            }
            systemConfiguration.MachineDetails.UpdateChangeOnClient = true;

            await _unitOfWork.Commit();
            return await Result.SuccessAsync("Machine updated successfully");
        }
        catch (Exception e)
        {
            await _unitOfWork.Rollback();
            return await Result.FailAsync(e.Message);
        }
    }

    public async Task<IResult> DeleteSystemConfiguration(SystemConfiguration systemConfiguration, string assetTag)
    {
        try
        {
            if (await _unitOfWork.GetRepository<MachineInfo>().GetByIdAsync(assetTag) is null)
                throw new Exception("Machine does not exist in database");
            await _unitOfWork.GetRepository<MachineInfo>().DeleteAsync(systemConfiguration.MachineDetails);

            foreach (var motherboard in systemConfiguration.Motherboards)
            {
                if (await _unitOfWork.GetRepository<MotherboardInfo>().GetByIdAsync(motherboard.OemSerialNo) is null)
                    throw new Exception("Motherboard does not exist to delete");
                await _unitOfWork.GetRepository<MotherboardInfo>().DeleteAsync(motherboard);
            }

            foreach (var keyboard in systemConfiguration.Keyboards)
            {
                if (await _unitOfWork.GetRepository<KeyboardInfo>().GetByIdAsync(keyboard.OemSerialNo) is null)
                    throw new Exception("Keyboard does not exist to delete");
                await _unitOfWork.GetRepository<KeyboardInfo>().DeleteAsync(keyboard);
            }

            foreach (var monitor in systemConfiguration.Monitors)
            {
                if (await _unitOfWork.GetRepository<MonitorInfo>().GetByIdAsync(monitor.OemSerialNo) is null)
                    throw new Exception("Monitor does not exist to delete");
                await _unitOfWork.GetRepository<MonitorInfo>().DeleteAsync(monitor);
            }

            foreach (var mouse in systemConfiguration.Mouses)
            {
                if (await _unitOfWork.GetRepository<MouseInfo>().GetByIdAsync(mouse.OemSerialNo) is null)
                    throw new Exception("Mouse does not exist to delete");
                await _unitOfWork.GetRepository<MouseInfo>().DeleteAsync(mouse);
            }
            
            await _unitOfWork.Commit();
            return await Result.SuccessAsync("Machine deleted successfully");
        }
        catch (Exception e)
        {
            await _unitOfWork.Rollback();
            return await Result.FailAsync(e.Message);
        }
    }
}