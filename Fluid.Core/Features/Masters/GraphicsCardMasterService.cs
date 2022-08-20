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
            Expression<Func<GraphicsCardInfo, GraphicsCardInfo>> expressionMap = info => new GraphicsCardInfo
            {
                OemSerialNo = info.OemSerialNo,
                Manufacturer = info.Manufacturer,
                Model = info.Model,
                Price = info.Price,
                PurchaseDate = info.PurchaseDate,
                MachineId = info.MachineId,
                Description = info.Description,
            };
            var specification = new GraphicsCardInfoSearchSpecification(searchString);
            if (orderBy?.Any() != true)
            {
                return await _unitOfWork.GetRepository<GraphicsCardInfo>().Entities.Specify(specification).Select(expressionMap).ToPaginatedListAsync(pageNumber, pageSize);
            }
            return await _unitOfWork.GetRepository<GraphicsCardInfo>().Entities.Specify(specification).OrderBy(string.Join(",", orderBy)).Select(expressionMap).ToPaginatedListAsync(pageNumber, pageSize);
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
            return graphicsCardInfo is not null ? Result<GraphicsCardInfo>.Success(graphicsCardInfo) : throw new Exception("GraphicsCard not found");
        }
        catch (Exception e)
        {
            return Result<GraphicsCardInfo>.Fail(e.Message);
        }
    }

    public async Task<Result<string>> AddAsync(GraphicsCardInfo model)
    {
        try
        {
            if (await _unitOfWork.GetRepository<GraphicsCardInfo>().GetByIdAsync(model.OemSerialNo) is not null)
                throw new Exception($"GraphicsCard with OEM Serial Number {model.OemSerialNo} already exists");
            var graphicsCardInfo = new GraphicsCardInfo
            {
                OemSerialNo = model.OemSerialNo,
                Manufacturer = model.Manufacturer,
                Model = model.Model,
                MachineId = model.MachineId,
                Description = model.Description,
                Price = model.Price,
                PurchaseDate = model.PurchaseDate
            };
            await _unitOfWork.GetRepository<GraphicsCardInfo>().AddAsync(graphicsCardInfo);
            await _unitOfWork.Commit();
            return Result<string>.Success(model.OemSerialNo, "Added GraphicsCard successfully");
        }
        catch (Exception e)
        {
            return Result<string>.Fail(e.Message);
        }
    }

    public async Task<Result<string>> EditAsync(GraphicsCardInfo model)
    {
        try
        {
            var oldGraphicsCardInfo = await _unitOfWork.GetRepository<GraphicsCardInfo>().GetByIdAsync(model.OemSerialNo);
            if (oldGraphicsCardInfo is null) throw new Exception("GraphicsCard not found");
            var updatedGraphicsCardInfo = new GraphicsCardInfo
            {
                OemSerialNo = model.OemSerialNo,
                Manufacturer = model.Manufacturer,
                Model = model.Model,
                MachineId = model.MachineId,
                Description = model.Description,
                Price = model.Price,
                PurchaseDate = model.PurchaseDate
            };
            await _unitOfWork.GetRepository<GraphicsCardInfo>().UpdateAsync(updatedGraphicsCardInfo, model.OemSerialNo);
            await _unitOfWork.Commit();
            return Result<string>.Success(model.OemSerialNo, "Updated GraphicsCard successfully");
        }
        catch (Exception e)
        {
            return Result<string>.Fail(e.Message);
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
            return Result<string>.Success(oemSerialNo);
        }
        catch (Exception e)
        {
            return Result<string>.Fail(e.Message);
        }
    }
}
