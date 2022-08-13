using Fluid.BgService.Models;

namespace Fluid.BgService.Services;

public class TechnicianCredentialsService
{
    private readonly WritableOptions<TechnicianCredentials> _options;

    public TechnicianCredentialsService(WritableOptions<TechnicianCredentials> options)
    {
        _options = options;
    }

    public TechnicianCredentials TechnicianCredentials { get; private set; }

    public void UpdateTechnicianCredentials(TechnicianCredentials credentials)
    {
        TechnicianCredentials = credentials;
        _options.Update(cred =>
        {
            cred.UserName = credentials.UserName;
            cred.Password = credentials.Password;
        });
    }
}
