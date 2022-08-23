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
            var specification = new KeyboardInfoSearchSpecification(searchString);
            if(orderBy?.Any() != true)
            {
                return await _unitOfWork.GetRepository<KeyboardInfo>().Entities.Specify(specification).ToPaginatedListAsync(pageNumber, pageSize);
            }
            return await _unitOfWork.GetRepository<KeyboardInfo>().Entities.Specify(specification).OrderBy(string.Join(",", orderBy)).ToPaginatedListAsync(pageNumber, pageSize);
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
            return keyboardInfo is not null ? await Result<KeyboardInfo>.SuccessAsync(keyboardInfo) : throw new Exception("Keyboard not found");
        }
        catch (Exception e)
        {
            return await Result<KeyboardInfo>.FailAsync(e.Message);
        }
    }

    public async Task<Result<string>> AddAsync(KeyboardInfo keyboardInfo)
    {
        try
        {
            if (await _unitOfWork.GetRepository<KeyboardInfo>().GetByIdAsync(keyboardInfo.OemSerialNo) is not null)
                throw new Exception($"Keyboard with OEM Serial Number {keyboardInfo.OemSerialNo} already exists");
            await _unitOfWork.GetRepository<KeyboardInfo>().AddAsync(keyboardInfo);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(keyboardInfo.OemSerialNo, "Added Keyboard successfully");
        }
        catch (Exception e)
        {
            return await Result<string>.FailAsync(e.Message);
        }
    }

    public async Task<Result<string>> EditAsync(KeyboardInfo keyboardInfo)
    {
        try
        {
            var oldKeyboardInfo = await _unitOfWork.GetRepository<KeyboardInfo>().GetByIdAsync(keyboardInfo.OemSerialNo);
            if (oldKeyboardInfo is null) throw new Exception("Keyboard not found");
            await _unitOfWork.GetRepository<KeyboardInfo>().UpdateAsync(keyboardInfo, keyboardInfo.OemSerialNo);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(keyboardInfo.OemSerialNo, "Updated Keyboard successfully");
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
            var keyboardInfo = await _unitOfWork.GetRepository<KeyboardInfo>().GetByIdAsync(oemSerialNo);
            if (keyboardInfo is null) throw new Exception("Keyboard not found");
            await _unitOfWork.GetRepository<KeyboardInfo>().DeleteAsync(keyboardInfo);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(oemSerialNo);
        }
        catch (Exception e)
        {
            return await Result<string>.FailAsync(e.Message);
        }
    }
}
