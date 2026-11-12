using LearnIn.Application.Interfaces.Persistence;
using LearnIn.Domain.Entities.Users;
using LearnIn.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnIn.Application.Features.Auth.Register;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result>
{
 private readonly IDataContext _context;
 
 public RegisterCommandHandler(IDataContext context)
  {
    _context = context;
  }

  public async Task<Result> Handle(RegisterCommand command, CancellationToken ct)
  {
      if (string.IsNullOrWhiteSpace(command.FirstName))
          return Result.Failure(Errors.FirstNameEmpty());
      
      if (string.IsNullOrWhiteSpace(command.LastName))
          return Result.Failure(Errors.LastNameEmpty());

      if (string.IsNullOrWhiteSpace(command.Email))
          return Result.Failure(Errors.EmailEmpty());

      if (!System.Text.RegularExpressions.Regex.IsMatch(command.Email,
              @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
          return Result.Failure(Errors.EmailInvalid());

      if (string.IsNullOrWhiteSpace(command.Password))
          return Result.Failure(Errors.PasswordEmpty());

      if (command.Password.Length < 8)
          return Result.Failure(Errors.PasswordWeak());
      
      var email = await _context.Users.FirstOrDefaultAsync(e => e.Email == command.Email);

      if (email != null)
      {
          return Result.Failure(Errors.EmailExists());
      }

      var hashedpass = BCrypt.Net.BCrypt.HashPassword(command.Password);

      User user = new User
      {
          FirstName = command.FirstName,
          LastName = command.LastName,
          Email = command.Email,
          Password = hashedpass,
      };
      
      _context.Users.Add(user);
      
      await _context.SaveChangesAsync(ct);
      return Result.Success();
  }
}