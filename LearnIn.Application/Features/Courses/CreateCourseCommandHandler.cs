using LearnIn.Application.Interfaces.Persistence;
using LearnIn.Domain.Entities.Courses;
using LearnIn.Domain.Enums.Users;
using LearnIn.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnIn.Application.Features.Courses;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Result<int>>
{
    private readonly IDataContext _context;

    public CreateCourseCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            return Result<int>.Failure(Errors.ValidationFailed()); 
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == request.TeacherId, cancellationToken);

        if (user == null)
        {
            return Result<int>.Failure(Errors.UserNotFound());
        }

        if (user.Role != UserEnums.Teacher)
        {
            return Result<int>.Failure(Errors.NotATeacher());
        }

        var course = new Course
        {
            Title = request.Title,
            Description = request.Description,
            PromoVideoUrl = request.PromoVideoUrl,
            TeacherId = request.TeacherId
        };

        await _context.Courses.AddAsync(course, cancellationToken);
        var result = await _context.SaveChangesAsync(cancellationToken);

        return result > 0 
            ? Result<int>.Success(course.Id) 
            : Result<int>.Failure(Errors.ServerError());
    }
}