using LearnIn.Domain.Results;
using MediatR;

namespace LearnIn.Application.Features.Auth.Login;

public record LoginCommand
(
    string Email,
    string Password
) : IRequest<Result<LoginResponse>>;