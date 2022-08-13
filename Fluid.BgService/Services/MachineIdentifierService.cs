using Fluid.BgService.Models;

namespace Fluid.BgService.Services;

public class MachineIdentifierService
{
    private readonly WritableOptions<MachineIdentifier> _options;

    public MachineIdentifierService(WritableOptions<MachineIdentifier> options)
    {
        MachineIdentifier = options.Value;
        _options = options;
    }

    public MachineIdentifier MachineIdentifier { get; private set; }

    public void SetMachineIdentifier(MachineIdentifier Model)
    {
        MachineIdentifier = Model;
        _options.Update(machineIdentifier => { machineIdentifier.ServerAddress = Model.ServerAddress; machineIdentifier.AssetTag = Model.AssetTag; });
    }

    public void SetAssetTag(string assetTag)
    {
        //TODO: AssetTag Validation
        MachineIdentifier.AssetTag = assetTag;
        _options.Update(machineIdentifier => { machineIdentifier.AssetTag = assetTag; machineIdentifier.ServerAddress = MachineIdentifier.ServerAddress; });
    }

    public void SetServerAddress(string serverAddress)
    {
        MachineIdentifier.ServerAddress = serverAddress;
        _options.Update(machineIdentifier => { machineIdentifier.ServerAddress = serverAddress; machineIdentifier.AssetTag = MachineIdentifier.AssetTag; });
    }
}
