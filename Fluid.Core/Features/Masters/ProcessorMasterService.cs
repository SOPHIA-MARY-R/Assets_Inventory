using Fluid.Core.Extensions;
using Fluid.Core.Specifications;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters;

public class ProcessorMasterService : IProcessorMasterService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProcessorMasterService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<ProcessorModel>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        try
        {
            Expression<Func<ProcessorInfo, ProcessorModel>> expressionMap = info => new ProcessorModel
            {
                ProcessorId = info.ProcessorId,
                Name = info.Name,
                Manufacturer = info.Manufacturer,
                Architecture = info.Architecture,
                Family = info.Family,
                NumberOfLogicalProcessors = info.NumberOfLogicalProcessors,
                NumberOfCores = info.NumberOfCores,
                ThreadCount = info.ThreadCount,
                MaxClockSpeed = info.MaxClockSpeed,
                Price = info.Price,
                PurchaseDate = info.PurchaseDate,
                MachineId = info.MachineId,
                Description = info.Description,
                UseStatus = info.UseStatus,
            };
            var specification = new ProcessorInfoSearchSpecification(searchString);
            if (orderBy?.Any() != true)
            {
                return await _unitOfWork.GetRepository<ProcessorInfo>().Entities.Specify(specification).Select(expressionMap).ToPaginatedListAsync(pageNumber, pageSize);
            }
            return await _unitOfWork.GetRepository<ProcessorInfo>().Entities.Specify(specification).OrderBy(string.Join(",", orderBy)).Select(expressionMap).ToPaginatedListAsync(pageNumber, pageSize);
        }
        catch (Exception e)
        {
            return PaginatedResult<ProcessorModel>.Failure(new List<string> { e.Message });
        }
    }

    public async Task<Result<ProcessorInfo>> GetByIdAsync(string processorId)
    {
        try
        {
            var processorInfo = await _unitOfWork.GetRepository<ProcessorInfo>().GetByIdAsync(processorId);
            return processorInfo is not null ? Result<ProcessorInfo>.Success(processorInfo) : throw new Exception("Processor not found");
        }
        catch (Exception e)
        {
            return Result<ProcessorInfo>.Fail(e.Message);
        }
    }

    public async Task<Result<string>> AddAsync(ProcessorModel model)
    {
        try
        {
            if (await _unitOfWork.GetRepository<ProcessorInfo>().GetByIdAsync(model.ProcessorId) is not null)
                throw new Exception($"Processor with ProcessorId {model.ProcessorId} already exists");
            var processorInfo = new ProcessorInfo
            {
                ProcessorId = model.ProcessorId,
                Name = model.Name,
                Architecture = model.Architecture,
                Family = model.Family,
                NumberOfCores = model.NumberOfCores,
                NumberOfLogicalProcessors = model.NumberOfLogicalProcessors,
                ThreadCount = model.ThreadCount,
                MaxClockSpeed = model.MaxClockSpeed,
                Manufacturer = model.Manufacturer,
                UseStatus = model.UseStatus,
                MachineId = model.MachineId,
                Description = model.Description,
                Price = model.Price,
                PurchaseDate = model.PurchaseDate
            };
            await _unitOfWork.GetRepository<ProcessorInfo>().AddAsync(processorInfo);
            await _unitOfWork.Commit();
            return Result<string>.Success(model.ProcessorId, "Added Processor successfully");
        }
        catch (Exception e)
        {
            return Result<string>.Fail(e.Message);
        }
    }

    public async Task<Result<string>> EditAsync(ProcessorModel model)
    {
        try
        {
            var oldProcessorInfo = await _unitOfWork.GetRepository<ProcessorInfo>().GetByIdAsync(model.ProcessorId);
            if (oldProcessorInfo is null) throw new Exception("Processor not found");
            var updatedProcessorInfo = new ProcessorInfo
            {
                ProcessorId = model.ProcessorId,
                Manufacturer = model.Manufacturer,
                UseStatus = model.UseStatus,
                Name = model.Name,
                Family = model.Family,
                Architecture = model.Architecture,
                NumberOfCores = model.NumberOfCores,
                NumberOfLogicalProcessors = model.NumberOfLogicalProcessors,
                MaxClockSpeed = model.MaxClockSpeed,
                ThreadCount = model.ThreadCount,
                MachineId = model.MachineId,
                Description = model.Description,
                Price = model.Price,
                PurchaseDate = model.PurchaseDate
            };
            await _unitOfWork.GetRepository<ProcessorInfo>().UpdateAsync(updatedProcessorInfo, model.ProcessorId);
            await _unitOfWork.Commit();
            return Result<string>.Success(model.ProcessorId, "Updated Processor successfully");
        }
        catch (Exception e)
        {
            return Result<string>.Fail(e.Message);
        }
    }

    public async Task<Result<string>> DeleteAsync(string processorId)
    {
        try
        {
            var processorInfo = await _unitOfWork.GetRepository<ProcessorInfo>().GetByIdAsync(processorId);
            if (processorInfo is null) throw new Exception("Processor not found");
            await _unitOfWork.GetRepository<ProcessorInfo>().DeleteAsync(processorInfo);
            await _unitOfWork.Commit();
            return Result<string>.Success(processorId);
        }
        catch (Exception e)
        {
            return Result<string>.Fail(e.Message);
        }
    }
}
