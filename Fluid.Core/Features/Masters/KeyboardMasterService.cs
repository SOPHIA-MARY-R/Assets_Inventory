using Fluid.Core.Extensions;
using Fluid.Core.Specifications;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters;

public class KeyboardMasterService : IKeyboardMasterService
{
    private readonly IUnitOfWork _unitOfWork;

    public KeyboardMasterService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<KeyboardInfo>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        try
        {
            Expression<Func<KeyboardInfo, KeyboardInfo>> expressionMap = info => new KeyboardInfo
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
            var specification = new KeyboardInfoSearchSpecification(searchString);
            if(orderBy?.Any() != true)
            {
                return await _unitOfWork.GetRepository<KeyboardInfo>().Entities.Specify(specification).Select(expressionMap).ToPaginatedListAsync(pageNumber, pageSize);
            }
            return await _unitOfWork.GetRepository<KeyboardInfo>().Entities.Specify(specification).OrderBy(string.Join(",", orderBy)).Select(expressionMap).ToPaginatedListAsync(pageNumber, pageSize);
        }
        catch(Exception e)
        {
            return PaginatedResult<KeyboardInfo>.Failure(new List<string> { e.Message });
        }
    }

    public async Task<Result<KeyboardInfo>> GetByIdAsync(string oemSerialNo)
    {
        try
        {
            var keyboardInfo = await _unitOfWork.GetRepository<KeyboardInfo>().GetByIdAsync(oemSerialNo);
            return keyboardInfo is not null ? Result<KeyboardInfo>.Success(keyboardInfo) : throw new Exception("Keyboard not found");
        }
        catch (Exception e)
        {
            return Result<KeyboardInfo>.Fail(e.Message);
        }
    }

    public async Task<Result<string>> AddAsync(KeyboardInfo model)
    {
        try
        {
            if (await _unitOfWork.GetRepository<KeyboardInfo>().GetByIdAsync(model.OemSerialNo) is not null)
                throw new Exception($"Keyboard with OEM Serial Number {model.OemSerialNo} already exists");
            var keyboardInfo = new KeyboardInfo
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
            await _unitOfWork.GetRepository<KeyboardInfo>().AddAsync(keyboardInfo);
            await _unitOfWork.Commit();
            return Result<string>.Success(model.OemSerialNo, "Added Keyboard successfully");
        }
        catch (Exception e)
        {
            return Result<string>.Fail(e.Message);
        }
    }

    public async Task<Result<string>> EditAsync(KeyboardInfo model)
    {
        try
        {
            var oldKeyboardInfo = await _unitOfWork.GetRepository<KeyboardInfo>().GetByIdAsync(model.OemSerialNo);
            if (oldKeyboardInfo is null) throw new Exception("Keyboard not found");
            var updatedKeyboardInfo = new KeyboardInfo
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
            await _unitOfWork.GetRepository<KeyboardInfo>().UpdateAsync(updatedKeyboardInfo, model.OemSerialNo);
            await _unitOfWork.Commit();
            return Result<string>.Success(model.OemSerialNo, "Updated Keyboard successfully");
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
            var keyboardInfo = await _unitOfWork.GetRepository<KeyboardInfo>().GetByIdAsync(oemSerialNo);
            if (keyboardInfo is null) throw new Exception("Keyboard not found");
            await _unitOfWork.GetRepository<KeyboardInfo>().DeleteAsync(keyboardInfo);
            await _unitOfWork.Commit();
            return Result<string>.Success(oemSerialNo);
        }
        catch (Exception e)
        {
            return Result<string>.Fail(e.Message);
        }
    }
}
