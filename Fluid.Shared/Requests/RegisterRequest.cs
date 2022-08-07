namespace Fluid.Shared.Requests;

public class RegisterRequest
{
    [Required]
    public string UserName { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    [Required]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }
}
