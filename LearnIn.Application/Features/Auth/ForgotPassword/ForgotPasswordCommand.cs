using LearnIn.Domain.Results;
using MediatR;

namespace LearnIn.Application.Features.Auth.ForgotPassword;

public sealed record ForgotPasswordCommand(string Email) : IRequest<Result<bool>>;