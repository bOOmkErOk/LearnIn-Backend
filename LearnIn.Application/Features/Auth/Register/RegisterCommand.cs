using LearnIn.Domain.Results;
using MediatR;

namespace LearnIn.Application.Features.Auth.Register;

public record RegisterCommand
(
    string FirstName,
    string LastName,
    string Email,
    string Password
    ) : IRequest<Result>;