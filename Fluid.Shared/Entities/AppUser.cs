using Microsoft.AspNetCore.Identity;

namespace Fluid.Shared.Entities;

public class AppUser : IdentityUser
{
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; } = DateTime.MinValue;
}
