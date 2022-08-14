namespace Fluid.Core.Interfaces;

public interface ICurrentUserService
{
    public string UserId { get; }
    public string UserName { get; set; }
}