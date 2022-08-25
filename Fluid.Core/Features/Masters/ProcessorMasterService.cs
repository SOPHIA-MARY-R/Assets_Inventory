using Fluid.Core.Extensions;
using Fluid.Core.Specifications;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters;

public class ProcessorMasterService : IProcessorMasterService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProcessorMasterService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<ProcessorInfo>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        try
        {
            var specification = new ProcessorInfoSearchSpecification(searchString);
            if (orderBy?.Any() != true)
            {
                return await _unitOfWork.GetRepository<ProcessorInfo>().Entities.Specify(specification).ToPaginatedListAsync(pageNumber, pageSize);
            }
            return await _unitOfWork.GetRepository<ProcessorInfo>().Entities.Specify(specification).OrderBy(string.Join(",", orderBy)).ToPaginatedListAsync(pageNumber, pageSize);
        }
        catch (Exception e)
        {
            return PaginatedResult<ProcessorInfo>.Failure(new List<string> { e.Message });
        }
    }

    public async Task<Result<ProcessorInfo>> GetByIdAsync(string oemSerialNo)
    {
        try
        {
            var processorInfo = await _unitOfWork.GetRepository<ProcessorInfo>().GetByIdAsync(oemSerialNo);
            return processorInfo is not null ? await Result<ProcessorInfo>.SuccessAsync(processorInfo) : throw new Exception("Processor not found");
        }
        catch (Exception e)
        {
            return await Result<ProcessorInfo>.FailAsync(e.Message);
        }
    }

    public async Task<Result<string>> AddAsync(ProcessorInfo processorInfo)
    {
        try
        {
            if (await _unitOfWork.GetRepository<ProcessorInfo>().GetByIdAsync(processorInfo.ProcessorId) is not null)
                throw new Exception($"Processor with ProcessorId {processorInfo.ProcessorId} already exists");
            await _unitOfWork.GetRepository<ProcessorInfo>().AddAsync(processorInfo);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(processorInfo.ProcessorId, "Added Processor successfully");
        }
        catch (Exception e)
        {
            return await Result<string>.FailAsync(e.Message);
        }
    }

    public async Task<Result<string>> EditAsync(ProcessorInfo processorInfo)
    {
        try
        {
            var oldProcessorInfo = await _unitOfWork.GetRepository<ProcessorInfo>().GetByIdAsync(processorInfo.ProcessorId);
            if (oldProcessorInfo is null) throw new Exception("Processor not found");
            await _unitOfWork.GetRepository<ProcessorInfo>().UpdateAsync(processorInfo, processorInfo.ProcessorId);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(processorInfo.ProcessorId, "Updated Processor successfully");
        }
        catch (Exception e)
        {
            return await Result<string>.FailAsync(e.Message);
        }
    }

    public async Task<Result<string>> DeleteAsync(string oemSerialNo)
    {
        try
        {
            var processorInfo = await _unitOfWork.GetRepository<ProcessorInfo>().GetByIdAsync(oemSerialNo);
            if (processorInfo is null) throw new Exception("Processor not found");
            await _unitOfWork.GetRepository<ProcessorInfo>().DeleteAsync(processorInfo);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(oemSerialNo);
        }
        catch (Exception e)
        {
            return await Result<string>.FailAsync(e.Message);
        }
    }
}
