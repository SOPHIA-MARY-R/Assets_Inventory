using Fluid.Shared.Entities;

namespace Fluid.Core.Features;
    
public class PurchaseOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public PurchaseOrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult<PurchaseInfo>> GetPurchaseInfo(string id)
    {
        try
        {
            var feedLog = await _unitOfWork.GetRepository<PurchaseInfo>()
                .Entities.Include(x => x.PurchaseItems).FirstOrDefaultAsync(x => x.InvoiceNo == id);
            if (feedLog is null)
                throw new Exception("The Requested Log record is not found");
            return await Result<PurchaseInfo>.SuccessAsync();
        }
        catch (Exception e)
        {
            return await Result<PurchaseInfo>.FailAsync(e.Message);
        }
    }
}