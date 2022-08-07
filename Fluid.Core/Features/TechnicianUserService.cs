using Fluid.Shared.Entities;
using Fluid.Shared.Requests;
using Fluid.Shared.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;

namespace Fluid.Core.Features;

public class TechnicianUserService : ITechnicianUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;

    public TechnicianUserService(UserManager<AppUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<Result<LoginResponse>> Login(LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null)
        {
            return Result<LoginResponse>.Fail("User Not Found.");
        }
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isPasswordValid)
        {
            return Result<LoginResponse>.Fail("Invalid Credentials.");
        }
        user.RefreshToken = GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
        await _userManager.UpdateAsync(user);
        var response = new LoginResponse { Token = GenerateJwtToken(user), RefreshToken = user.RefreshToken, RefreshTokenExpiryTime = user.RefreshTokenExpiryTime };
        return Result<LoginResponse>.Success(response, "Login successful");
    }

    private string GenerateJwtToken(AppUser user)
    {
        var secret = Encoding.UTF8.GetBytes(_configuration.GetSection("AppConfiguration:Secret").Value);
        var securityKey = new SymmetricSecurityKey(secret);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName)
        };
        var jwtToken = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: credentials);
        var jwtTokenString = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return jwtTokenString;
    }

    public async Task<IResult> RegisterAsync(RegisterRequest request)
    {
        if (await _userManager.FindByNameAsync(request.UserName) is not null)
        {
            return Result.Fail($"Username {request.UserName} already exists");
        }
        if (await _userManager.FindByEmailAsync(request.Email) is not null)
        {
            return Result.Fail($"Email {request.Email} is already registered");
        }
        var user = new AppUser
        {
            UserName = request.UserName,
            Email = request.Email,
            EmailConfirmed = true,
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return Result.Fail(result.Errors.Select(x => x.Description).ToList());
        }
        return Result.Success($"User {request.UserName} registered successfully");
    }

    public async Task<IResult> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
        {
            // Don't reveal that the user does not exist or is not confirmed
            return await Result.FailAsync("An Error has occurred!");
        }
        var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(await _userManager.GeneratePasswordResetTokenAsync(user)));
        var endpointUri = new Uri($"{origin}/account/reset-password");
        var passwordResetURL = QueryHelpers.AddQueryString(endpointUri.ToString(), "Token", code);
        var mailRequest = new MailRequest
        {
            To = request.Email,
            Subject = "Reset Password",
            Body = $"Please reset your password by <a href=\"{HtmlEncoder.Default.Encode(passwordResetURL)}\">clicking here</a>."
        };
        //TODO: Add a MailService and send the mail
        Console.WriteLine($"Reset Link for {request.Email}: {passwordResetURL}");
        return await Result.SuccessAsync("Password Reset Mail has been sent to your authorized Email.");
    }

    public async Task<Result<LoginResponse>> RefreshToken(RefreshTokenRequest request)
    {
        if (request is null)
        {
            return await Result<LoginResponse>.FailAsync("Invalid Client Token.");
        }
        var userPrincipal = GetPrincipalFromExpiredToken(request.Token);
        var userName = userPrincipal.FindFirstValue(ClaimTypes.Name);
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
            return await Result<LoginResponse>.FailAsync("User Not Found.");
        if (user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            return await Result<LoginResponse>.FailAsync("Invalid Client Token.");
        var token = GenerateJwtToken(user);
        user.RefreshToken = GenerateRefreshToken();
        await _userManager.UpdateAsync(user);
        var response = new LoginResponse { Token = token, RefreshToken = user.RefreshToken, RefreshTokenExpiryTime = user.RefreshTokenExpiryTime };
        return await Result<LoginResponse>.SuccessAsync(response, "Refreshed Token");
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppConfiguration:Secret").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
            StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
