using LearnIn.Application.Features.Auth.Login;
using LearnIn.Domain.Results;
using MediatR;

namespace LearnIn.Application.Features.Users.BecomeTeacher;

public record BecomeTeacherCommand(int UserId) : IRequest<Result<LoginResponse>>;