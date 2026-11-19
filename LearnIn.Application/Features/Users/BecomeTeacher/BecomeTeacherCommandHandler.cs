using LearnIn.Application.Features.Auth.Login;
using LearnIn.Application.Interfaces.Persistence;
using LearnIn.Application.Interfaces.Security;
using LearnIn.Domain.Enums.Users;
using LearnIn.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnIn.Application.Features.Users.BecomeTeacher;

public class BecomeTeacherCommandHandler : IRequestHandler<BecomeTeacherCommand, Result<LoginResponse>>
{
    private readonly IDataContext _context;
    private readonly IJwtTokenService _jwtTokenService;

    public BecomeTeacherCommandHandler(IDataContext context, IJwtTokenService jwtTokenService)
    {
        _context = context;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<Result<LoginResponse>> Handle(BecomeTeacherCommand command, CancellationToken ct)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == command.UserId, ct);

        if (user == null)
            return Result<LoginResponse>.Failure(Errors.UserNotFound());

        if (user.Role == UserEnums.Teacher)
            return Result<LoginResponse>.Failure(new Errors(400, "You are already registered as a Teacher"));

        if (user.Role == UserEnums.Admin)
            return Result<LoginResponse>.Failure(Errors.Forbidden());

        // Flip the role to Teacher
        user.Role = UserEnums.Teacher;

        var result = await _context.SaveChangesAsync(ct);

        if (result <= 0)
            return Result<LoginResponse>.Failure(Errors.ServerError());

        // Generate a fresh token so the new Teacher role claim takes effect immediately
        var newToken = _jwtTokenService.GenerateAccessToken(user);
        
        var response = new LoginResponse
        {
            Token = newToken,
            UserId = user.Id,
            FirstName = user.FirstName,
            Role = user.Role
        };

        return Result<LoginResponse>.Success(response);
    }
}