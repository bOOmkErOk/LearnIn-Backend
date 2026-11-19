using LearnIn.Domain.Results;
using MediatR;

namespace LearnIn.Application.Features.Auth.VerifyEmail;

public record VerifyEmailCommand
(
    string Email,
    string Code
) : IRequest<Result<int>>;
