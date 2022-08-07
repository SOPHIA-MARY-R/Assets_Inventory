using Fluid.Core.Extensions;
using Fluid.Core.Specifications;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters;

public class MouseMasterService : IMouseMasterService
{
    private readonly IUnitOfWork _unitOfWork;

    public MouseMasterService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<MouseModel>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        try
        {
            Expression<Func<MouseInfo, MouseModel>> expressionMap = info => new MouseModel
            {
                OemSerialNo = info.OemSerialNo,
                Manufacturer = info.Manufacturer,
                Model = info.Model,
                Price = info.Price,
                PurchaseDate = info.PurchaseDate,
                MachineId = info.MachineId,
                IsWireless = info.IsWireless,
                Description = info.Description,
            };
            var specification = new MouseInfoSearchSpecification(searchString);
            if (orderBy?.Any() != true)
            {
                return await _unitOfWork.GetRepository<MouseInfo>().Entities.Specify(specification).Select(expressionMap).ToPaginatedListAsync(pageNumber, pageSize);
            }
            return await _unitOfWork.GetRepository<MouseInfo>().Entities.Specify(specification).OrderBy(string.Join(",", orderBy)).Select(expressionMap).ToPaginatedListAsync(pageNumber, pageSize);
        }
        catch (Exception e)
        {
            return PaginatedResult<MouseModel>.Failure(new List<string> { e.Message });
        }
    }

    public async Task<Result<MouseInfo>> GetByIdAsync(string oemSerialNo)
    {
        try
        {
            var MouseInfo = await _unitOfWork.GetRepository<MouseInfo>().GetByIdAsync(oemSerialNo);
            return MouseInfo is not null ? Result<MouseInfo>.Success(MouseInfo) : throw new Exception("Mouse not found");
        }
        catch (Exception e)
        {
            return Result<MouseInfo>.Fail(e.Message);
        }
    }

    public async Task<Result<string>> AddAsync(MouseModel model)
    {
        try
        {
            if (await _unitOfWork.GetRepository<MouseInfo>().GetByIdAsync(model.OemSerialNo) is not null)
                throw new Exception($"Mouse with OEM Serial Number {model.OemSerialNo} already exists");
            var MouseInfo = new MouseInfo
            {
                OemSerialNo = model.OemSerialNo,
                Manufacturer = model.Manufacturer,
                Model = model.Model,
                MachineId = model.MachineId,
                IsWireless = model.IsWireless,
                Description = model.Description,
                Price = model.Price,
                PurchaseDate = model.PurchaseDate
            };
            await _unitOfWork.GetRepository<MouseInfo>().AddAsync(MouseInfo);
            await _unitOfWork.Commit();
            return Result<string>.Success(model.OemSerialNo, "Added Mouse successfully");
        }
        catch (Exception e)
        {
            return Result<string>.Fail(e.Message);
        }
    }

    public async Task<Result<string>> EditAsync(MouseModel model)
    {
        try
        {
            var oldMouseInfo = await _unitOfWork.GetRepository<MouseInfo>().GetByIdAsync(model.OemSerialNo);
            if (oldMouseInfo is null) throw new Exception("Mouse not found");
            var updatedMouseInfo = new MouseInfo
            {
                OemSerialNo = model.OemSerialNo,
                Manufacturer = model.Manufacturer,
                Model = model.Model,
                MachineId = model.MachineId,
                IsWireless = model.IsWireless,
                Description = model.Description,
                Price = model.Price,
                PurchaseDate = model.PurchaseDate
            };
            await _unitOfWork.GetRepository<MouseInfo>().UpdateAsync(updatedMouseInfo, model.OemSerialNo);
            await _unitOfWork.Commit();
            return Result<string>.Success(model.OemSerialNo, "Updated Mouse successfully");
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
            var MouseInfo = await _unitOfWork.GetRepository<MouseInfo>().GetByIdAsync(oemSerialNo);
            if (MouseInfo is null) throw new Exception("Mouse not found");
            await _unitOfWork.GetRepository<MouseInfo>().DeleteAsync(MouseInfo);
            await _unitOfWork.Commit();
            return Result<string>.Success(oemSerialNo);
        }
        catch (Exception e)
        {
            return Result<string>.Fail(e.Message);
        }
    }
}
