using Fluid.Core.Extensions;
using Fluid.Core.Specifications;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters;

public class MotherboardMasterService : IMotherboardMasterService
{
    private readonly IUnitOfWork _unitOfWork;

    public MotherboardMasterService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<MotherboardModel>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        try
        {
            Expression<Func<MotherboardInfo, MotherboardModel>> expressionMap = info => new MotherboardModel
            {
                OemSerialNo = info.OemSerialNo,
                Manufacturer = info.Manufacturer,
                Model = info.Model,
                Price = info.Price,
                PurchaseDate = info.PurchaseDate,
                MachineId = info.MachineId,
                Description = info.Description,
            };
            var specification = new MotherboardInfoSearchSpecification(searchString);
            if (orderBy?.Any() != true)
            {
                return await _unitOfWork.GetRepository<MotherboardInfo>().Entities.Specify(specification).Select(expressionMap).ToPaginatedListAsync(pageNumber, pageSize);
            }
            return await _unitOfWork.GetRepository<MotherboardInfo>().Entities.Specify(specification).OrderBy(string.Join(",", orderBy)).Select(expressionMap).ToPaginatedListAsync(pageNumber, pageSize);
        }
        catch (Exception e)
        {
            return PaginatedResult<MotherboardModel>.Failure(new List<string> { e.Message });
        }
    }

    public async Task<Result<MotherboardInfo>> GetByIdAsync(string oemSerialNo)
    {
        try
        {
            var motherboardInfo = await _unitOfWork.GetRepository<MotherboardInfo>().GetByIdAsync(oemSerialNo);
            return motherboardInfo is not null ? Result<MotherboardInfo>.Success(motherboardInfo) : throw new Exception("Motherboard not found");
        }
        catch (Exception e)
        {
            return Result<MotherboardInfo>.Fail(e.Message);
        }
    }

    public async Task<Result<string>> AddAsync(MotherboardModel model)
    {
        try
        {
            if (await _unitOfWork.GetRepository<MotherboardInfo>().GetByIdAsync(model.OemSerialNo) is not null)
                throw new Exception($"Motherboard with OEM Serial Number {model.OemSerialNo} already exists");
            var motherboardInfo = new MotherboardInfo
            {
                OemSerialNo = model.OemSerialNo,
                Manufacturer = model.Manufacturer,
                Model = model.Model,
                MachineId = model.MachineId,
                Description = model.Description,
                Price = model.Price,
                PurchaseDate = model.PurchaseDate
            };
            await _unitOfWork.GetRepository<MotherboardInfo>().AddAsync(motherboardInfo);
            await _unitOfWork.Commit();
            return Result<string>.Success(model.OemSerialNo, "Added Motherboard successfully");
        }
        catch (Exception e)
        {
            return Result<string>.Fail(e.Message);
        }
    }

    public async Task<Result<string>> EditAsync(MotherboardModel model)
    {
        try
        {
            var oldMotherboardInfo = await _unitOfWork.GetRepository<MotherboardInfo>().GetByIdAsync(model.OemSerialNo);
            if (oldMotherboardInfo is null) throw new Exception("Motherboard not found");
            var updatedMotherboardInfo = new MotherboardInfo
            {
                OemSerialNo = model.OemSerialNo,
                Manufacturer = model.Manufacturer,
                Model = model.Model,
                MachineId = model.MachineId,
                Description = model.Description,
                Price = model.Price,
                PurchaseDate = model.PurchaseDate
            };
            await _unitOfWork.GetRepository<MotherboardInfo>().UpdateAsync(updatedMotherboardInfo, model.OemSerialNo);
            await _unitOfWork.Commit();
            return Result<string>.Success(model.OemSerialNo, "Updated Motherboard successfully");
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
            var motherboardInfo = await _unitOfWork.GetRepository<MotherboardInfo>().GetByIdAsync(oemSerialNo);
            if (motherboardInfo is null) throw new Exception("Motherboard not found");
            await _unitOfWork.GetRepository<MotherboardInfo>().DeleteAsync(motherboardInfo);
            await _unitOfWork.Commit();
            return Result<string>.Success(oemSerialNo);
        }
        catch (Exception e)
        {
            return Result<string>.Fail(e.Message);
        }
    }
}
