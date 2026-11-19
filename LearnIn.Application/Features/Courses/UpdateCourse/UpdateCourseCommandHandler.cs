using LearnIn.Application.Interfaces.Persistence;
using LearnIn.Domain.Entities.Courses;
using LearnIn.Domain.Enums.Users;
using LearnIn.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnIn.Application.Features.Courses.UpdateCourse;

public sealed class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, Result<int>>
{
    private readonly IDataContext _context;
    
    public UpdateCourseCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(UpdateCourseCommand command, CancellationToken ct)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == command.CourseId, ct);

        if (course == null)
        {
            return Result<int>.Failure(Errors.CourseNotFound());
        }

        if (course.TeacherId != command.TeacherId)
        {
            return Result<int>.Failure(Errors.Forbidden());
        }

        course.Title = command.CourseTitle;
        course.Description = command.CourseDescription;
        course.PromoVideoUrl = command.PromoVideoUrl;
        
        
        await _context.SaveChangesAsync(ct);
        
        return Result<int>.Success(course.Id);

    }
    
}