using LearnIn.Domain.Enums.Users;

namespace LearnIn.Application.Features.Auth.Login;

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public int UserId { get; set; }
    
    public string FirstName { get; set; } = string.Empty;
    
    public UserEnums Role { get; set; }
    
}
