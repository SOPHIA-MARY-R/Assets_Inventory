using Fluid.Core.Extensions;
using Fluid.Core.Specifications;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters;

public class GraphicsCardMasterService : IGraphicsCardMasterService
{
    private readonly IUnitOfWork _unitOfWork;

    public GraphicsCardMasterService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<GraphicsCardInfo>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        try
        {
            var specification = new GraphicsCardInfoSearchSpecification(searchString);
            if (orderBy?.Any() != true)
            {
                return await _unitOfWork.GetRepository<GraphicsCardInfo>().Entities.Specify(specification).ToPaginatedListAsync(pageNumber, pageSize);
            }
            return await _unitOfWork.GetRepository<GraphicsCardInfo>().Entities.Specify(specification).OrderBy(string.Join(",", orderBy)).ToPaginatedListAsync(pageNumber, pageSize);
        }
        catch (Exception e)
        {
            return PaginatedResult<GraphicsCardInfo>.Failure(new List<string> { e.Message });
        }
    }

    public async Task<Result<GraphicsCardInfo>> GetByIdAsync(string oemSerialNo)
    {
        try
        {
            var graphicsCardInfo = await _unitOfWork.GetRepository<GraphicsCardInfo>().GetByIdAsync(oemSerialNo);
            return graphicsCardInfo is not null ? await Result<GraphicsCardInfo>.SuccessAsync(graphicsCardInfo) : throw new Exception("GraphicsCard not found");
        }
        catch (Exception e)
        {
            return await Result<GraphicsCardInfo>.FailAsync(e.Message);
        }
    }

    public async Task<Result<string>> AddAsync(GraphicsCardInfo graphicsCardInfo)
    {
        try
        {
            if (await _unitOfWork.GetRepository<GraphicsCardInfo>().GetByIdAsync(graphicsCardInfo.OemSerialNo) is not null)
                throw new Exception($"GraphicsCard with OEM Serial Number {graphicsCardInfo.OemSerialNo} already exists");
            await _unitOfWork.GetRepository<GraphicsCardInfo>().AddAsync(graphicsCardInfo);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(graphicsCardInfo.OemSerialNo, "Added GraphicsCard successfully");
        }
        catch (Exception e)
        {
            return await Result<string>.FailAsync(e.Message);
        }
    }

    public async Task<Result<string>> EditAsync(GraphicsCardInfo graphicsCardInfo)
    {
        try
        {
            var oldGraphicsCardInfo = await _unitOfWork.GetRepository<GraphicsCardInfo>().GetByIdAsync(graphicsCardInfo.OemSerialNo);
            if (oldGraphicsCardInfo is null) throw new Exception("GraphicsCard not found");
            await _unitOfWork.GetRepository<GraphicsCardInfo>().UpdateAsync(graphicsCardInfo, graphicsCardInfo.OemSerialNo);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(graphicsCardInfo.OemSerialNo, "Updated GraphicsCard successfully");
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
            var graphicsCardInfo = await _unitOfWork.GetRepository<GraphicsCardInfo>().GetByIdAsync(oemSerialNo);
            if (graphicsCardInfo is null) throw new Exception("GraphicsCard not found");
            await _unitOfWork.GetRepository<GraphicsCardInfo>().DeleteAsync(graphicsCardInfo);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(oemSerialNo);
        }
        catch (Exception e)
        {
            return await Result<string>.FailAsync(e.Message);
        }
    }
}
