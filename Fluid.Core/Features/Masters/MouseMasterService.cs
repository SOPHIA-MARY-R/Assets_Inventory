using Fluid.Core.Extensions;
using Fluid.Core.Specifications;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters;

public class MouseMasterService : IMouseMasterService
{
    private readonly IUnitOfWork _unitOfWork;

    public MouseMasterService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<MouseInfo>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        try
        {
            var specification = new MouseInfoSearchSpecification(searchString);
            if (orderBy?.Any() != true)
            {
                return await _unitOfWork.GetRepository<MouseInfo>().Entities.Specify(specification).ToPaginatedListAsync(pageNumber, pageSize);
            }
            return await _unitOfWork.GetRepository<MouseInfo>().Entities.Specify(specification).OrderBy(string.Join(",", orderBy)).ToPaginatedListAsync(pageNumber, pageSize);
        }
        catch (Exception e)
        {
            return PaginatedResult<MouseInfo>.Failure(new List<string> { e.Message });
        }
    }

    public async Task<Result<MouseInfo>> GetByIdAsync(string oemSerialNo)
    {
        try
        {
            var mouseInfo = await _unitOfWork.GetRepository<MouseInfo>().GetByIdAsync(oemSerialNo);
            return mouseInfo is not null ? await Result<MouseInfo>.SuccessAsync(mouseInfo) : throw new Exception("Mouse not found");
        }
        catch (Exception e)
        {
            return await Result<MouseInfo>.FailAsync(e.Message);
        }
    }

    public async Task<Result<string>> AddAsync(MouseInfo model)
    {
        try
        {
            if (await _unitOfWork.GetRepository<MouseInfo>().GetByIdAsync(model.OemSerialNo) is not null)
                throw new Exception($"Mouse with OEM Serial Number {model.OemSerialNo} already exists");
            await _unitOfWork.GetRepository<MouseInfo>().AddAsync(model);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(model.OemSerialNo, "Added Mouse successfully");
        }
        catch (Exception e)
        {
            return await Result<string>.FailAsync(e.Message);
        }
    }

    public async Task<Result<string>> EditAsync(MouseInfo model)
    {
        try
        {
            var oldMouseInfo = await _unitOfWork.GetRepository<MouseInfo>().GetByIdAsync(model.OemSerialNo);
            if (oldMouseInfo is null) throw new Exception("Mouse not found");
            await _unitOfWork.GetRepository<MouseInfo>().UpdateAsync(model, model.OemSerialNo);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(model.OemSerialNo, "Updated Mouse successfully");
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
            var mouseInfo = await _unitOfWork.GetRepository<MouseInfo>().GetByIdAsync(oemSerialNo);
            if (mouseInfo is null) throw new Exception("Mouse not found");
            await _unitOfWork.GetRepository<MouseInfo>().DeleteAsync(mouseInfo);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(oemSerialNo);
        }
        catch (Exception e)
        {
            return await Result<string>.FailAsync(e.Message);
        }
    }
}
