using Fluid.Core.Extensions;
using Fluid.Core.Specifications;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters;

public class SpeakerMasterService : ISpeakerMasterService
{
    private readonly IUnitOfWork _unitOfWork;

    public SpeakerMasterService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<SpeakerInfo>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        try
        {
            Expression<Func<SpeakerInfo, SpeakerInfo>> expressionMap = info => new SpeakerInfo
            {
                OemSerialNo = info.OemSerialNo,
                Manufacturer = info.Manufacturer,
                InputPorts = info.InputPorts,
                IsBlueTooth = info.IsBlueTooth,
                Model = info.Model,
                Price = info.Price,
                PurchaseDate = info.PurchaseDate,
                MachineId = info.MachineId,
                Description = info.Description,
            };
            var specification = new SpeakerInfoSearchSpecification(searchString);
            if (orderBy?.Any() != true)
            {
                return await _unitOfWork.GetRepository<SpeakerInfo>().Entities.Specify(specification).Select(expressionMap).ToPaginatedListAsync(pageNumber, pageSize);
            }
            return await _unitOfWork.GetRepository<SpeakerInfo>().Entities.Specify(specification).OrderBy(string.Join(",", orderBy)).Select(expressionMap).ToPaginatedListAsync(pageNumber, pageSize);
        }
        catch (Exception e)
        {
            return PaginatedResult<SpeakerInfo>.Failure(new List<string> { e.Message });
        }
    }

    public async Task<Result<SpeakerInfo>> GetByIdAsync(string oemSerialNo)
    {
        try
        {
            var speakerInfo = await _unitOfWork.GetRepository<SpeakerInfo>().GetByIdAsync(oemSerialNo);
            return speakerInfo is not null ? Result<SpeakerInfo>.Success(speakerInfo) : throw new Exception("Speaker not found");
        }
        catch (Exception e)
        {
            return Result<SpeakerInfo>.Fail(e.Message);
        }
    }

    public async Task<Result<string>> AddAsync(SpeakerInfo model)
    {
        try
        {
            if (await _unitOfWork.GetRepository<SpeakerInfo>().GetByIdAsync(model.OemSerialNo) is not null)
                throw new Exception($"Speaker with OEM Serial Number {model.OemSerialNo} already exists");
            var speakerInfo = new SpeakerInfo
            {
                OemSerialNo = model.OemSerialNo,
                Manufacturer = model.Manufacturer,
                Model = model.Model,
                InputPorts = model.InputPorts,
                IsBlueTooth = model.IsBlueTooth,
                MachineId = model.MachineId,
                Description = model.Description,
                Price = model.Price,
                PurchaseDate = model.PurchaseDate
            };
            await _unitOfWork.GetRepository<SpeakerInfo>().AddAsync(speakerInfo);
            await _unitOfWork.Commit();
            return Result<string>.Success(model.OemSerialNo, "Added Speaker successfully");
        }
        catch (Exception e)
        {
            return Result<string>.Fail(e.Message);
        }
    }

    public async Task<Result<string>> EditAsync(SpeakerInfo model)
    {
        try
        {
            var oldSpeakerInfo = await _unitOfWork.GetRepository<SpeakerInfo>().GetByIdAsync(model.OemSerialNo);
            if (oldSpeakerInfo is null) throw new Exception("Speaker not found");
            var updatedSpeakerInfo = new SpeakerInfo
            {
                OemSerialNo = model.OemSerialNo,
                Manufacturer = model.Manufacturer,
                Model = model.Model,
                InputPorts=model.InputPorts,
                IsBlueTooth=model.IsBlueTooth,
                MachineId = model.MachineId,
                Description = model.Description,
                Price = model.Price,
                PurchaseDate = model.PurchaseDate
            };
            await _unitOfWork.GetRepository<SpeakerInfo>().UpdateAsync(updatedSpeakerInfo, model.OemSerialNo);
            await _unitOfWork.Commit();
            return Result<string>.Success(model.OemSerialNo, "Updated Speaker successfully");
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
            var speakerInfo = await _unitOfWork.GetRepository<SpeakerInfo>().GetByIdAsync(oemSerialNo);
            if (speakerInfo is null) throw new Exception("Speaker not found");
            await _unitOfWork.GetRepository<SpeakerInfo>().DeleteAsync(speakerInfo);
            await _unitOfWork.Commit();
            return Result<string>.Success(oemSerialNo);
        }
        catch (Exception e)
        {
            return Result<string>.Fail(e.Message);
        }
    }
}
