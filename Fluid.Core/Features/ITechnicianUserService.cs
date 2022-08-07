using Fluid.Shared.Requests;
using Fluid.Shared.Responses;

namespace Fluid.Core.Features;

public interface ITechnicianUserService
{
    Task<IResult> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);

    Task<Result<LoginResponse>> Login(LoginRequest request);

    Task<Result<LoginResponse>> RefreshToken(RefreshTokenRequest request);

    Task<IResult> RegisterAsync(RegisterRequest request);
}