using LearnIn.Domain.Entities.Users;

namespace LearnIn.Application.Interfaces.Security;

public interface IJwtTokenService
{
    string GenerateAccessToken(User user);
}