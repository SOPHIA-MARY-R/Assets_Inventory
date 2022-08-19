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

    public async Task<SystemConfiguration> GetSystemConfiguration(string assetTag)
    {
        var sysConfig = new SystemConfiguration
        {
            MachineDetails = await _unitOfWork.GetRepository<MachineInfo>().GetByIdAsync(assetTag),
            Motherboard = await _unitOfWork.GetRepository<MotherboardInfo>().Entities
                .FirstOrDefaultAsync(x => x.MachineId == assetTag),
            PhysicalMemories = await _unitOfWork.GetRepository<PhysicalMemoryInfo>().Entities
                .Where(x => x.MachineId == assetTag).ToListAsync(),
            HardDisks = await _unitOfWork.GetRepository<HardDiskInfo>().Entities
                .Where(x => x.MachineId == assetTag)
                .ToListAsync(),
            Processors = await _unitOfWork.GetRepository<ProcessorInfo>().Entities
                .Where(x => x.MachineId == assetTag)
                .ToListAsync(),
            Mouse = await _unitOfWork.GetRepository<MouseInfo>().Entities
                .FirstOrDefaultAsync(x => x.MachineId == assetTag),
            Keyboard = await _unitOfWork.GetRepository<KeyboardInfo>().Entities
                .FirstOrDefaultAsync(x => x.MachineId == assetTag),
            Monitor = await _unitOfWork.GetRepository<MonitorInfo>().Entities
                .FirstOrDefaultAsync(x => x.MachineId == assetTag)
        };
        return sysConfig;
    }

    public async Task<IResult> AddSystemConfiguration(SystemConfiguration systemConfiguration)
    {
        try
        {
            if (await _unitOfWork.GetRepository<MachineInfo>()
                    .GetByIdAsync(systemConfiguration.MachineDetails.AssetTag) is not null)
                throw new Exception("Machine with the same asset tag already present");
            await _unitOfWork.GetRepository<MachineInfo>().AddAsync(systemConfiguration.MachineDetails);

            var existingMotherboard = await _unitOfWork.GetRepository<MotherboardInfo>()
                .GetByIdAsync(systemConfiguration.Motherboard.OemSerialNo);
            if (existingMotherboard is not null && !string.IsNullOrEmpty(existingMotherboard.MachineId))
                throw new Exception("The Selected Motherboard is already in use by another machine");
            await _unitOfWork.GetRepository<MotherboardInfo>().AddAsync(systemConfiguration.Motherboard);
            
            var existingKeyboard = await _unitOfWork.GetRepository<KeyboardInfo>()
                .GetByIdAsync(systemConfiguration.Keyboard.OemSerialNo);
            if (existingKeyboard is not null && !string.IsNullOrEmpty(existingKeyboard.MachineId))
                throw new Exception("The Selected Keyboard is already in use by another machine");
            await _unitOfWork.GetRepository<KeyboardInfo>().AddAsync(systemConfiguration.Keyboard);
            
            var existingMouse = await _unitOfWork.GetRepository<MouseInfo>()
                .GetByIdAsync(systemConfiguration.Mouse.OemSerialNo);
            if (existingMouse is not null && !string.IsNullOrEmpty(existingMouse.MachineId))
                throw new Exception("The Selected Mouse is already in use by another machine");
            await _unitOfWork.GetRepository<MouseInfo>().AddAsync(systemConfiguration.Mouse);
            
            await _unitOfWork.Commit();
            return await Result.SuccessAsync("Machine added successfully");
        }
        catch (Exception e)
        {
            await _unitOfWork.Rollback();
            return await Result.FailAsync(e.Message);
        }
    }

    public async Task<IResult> EditSystemConfiguration(SystemConfiguration systemConfiguration)
    {
        try
        {
            var assetTag = systemConfiguration.MachineDetails.AssetTag;
            if (await _unitOfWork.GetRepository<MachineInfo>().GetByIdAsync(assetTag) is null)
                throw new Exception("Machine does not exist in database");
            await _unitOfWork.GetRepository<MachineInfo>().AddAsync(systemConfiguration.MachineDetails);

            var motherboard = await _unitOfWork.GetRepository<MotherboardInfo>()
                .GetByIdAsync(systemConfiguration.Motherboard.OemSerialNo);
            if(motherboard is not null && string.IsNullOrEmpty(motherboard.MachineId))
                throw new Exception("The Selected Motherboard is already in use by another machine");
            //await _unitOfWork.GetRepository<MotherboardInfo>().AddAsync()
            
            var existingOldMotherboard = await _unitOfWork.GetRepository<MotherboardInfo>()
                .GetByIdAsync(systemConfiguration.Motherboard.OemSerialNo);
            if(existingOldMotherboard is not null)
                existingOldMotherboard.MachineId = assetTag;
            
            
            await _unitOfWork.Commit();
            return await Result.SuccessAsync("Machine updated successfully");
        }
        catch (Exception e)
        {
            await _unitOfWork.Rollback();
            return await Result.FailAsync(e.Message);
        }
    }
}