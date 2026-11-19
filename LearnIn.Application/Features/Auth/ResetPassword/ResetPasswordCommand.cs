using LearnIn.Domain.Results;
using MediatR;

namespace LearnIn.Application.Features.Auth.ResetPassword;

public sealed record ResetPasswordCommand(
    string Email,
    string NewPassword
) : IRequest<Result<bool>>;
