using Fluid.Shared.Constants;
using Fluid.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Fluid.Server;

public class DatabaseSeeder
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<DatabaseSeeder> _logger;

    public DatabaseSeeder(UserManager<AppUser> userManager, ILogger<DatabaseSeeder> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async void SeedAdminUser()
    {
        if (!await _userManager.Users.AnyAsync())
        {
            var user = new AppUser
            {
                UserName = UserConstants.AdminUserName,
                Email = UserConstants.AdminEmail,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, UserConstants.Password);
            if (result.Succeeded)
            {
                _logger.Log(LogLevel.Information, "Seeded Admin User Successfully");
            }
            else
            {
                foreach (var error in result.Errors.Select(x => x.Description))
                {
                    _logger.LogError(error);
                }
            }
        }
    }
}
