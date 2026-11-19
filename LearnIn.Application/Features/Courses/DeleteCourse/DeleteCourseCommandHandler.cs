using LearnIn.Application.Interfaces.Persistence;
using LearnIn.Domain.Enums.Users;
using LearnIn.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnIn.Application.Features.Courses.DeleteCourse;

public sealed class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, Result<int>>
{
    private readonly IDataContext _context;
    
    public DeleteCourseCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(DeleteCourseCommand command, CancellationToken ct)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == command.CourseId, ct);
        var teacher = await _context.Users.FirstOrDefaultAsync(u => u.Id == command.TeacherId, ct);
        if (course == null)
        {
            return Result<int>.Failure(Errors.CourseNotFound());
        }

        if (course.TeacherId != command.TeacherId)
        {
            return Result<int>.Failure(Errors.TeacherNotFound());
        }

        if (teacher!.Role != UserEnums.Teacher)
        {
            return Result<int>.Failure(Errors.TeacherNotFound());
        }
        
        _context.Courses.Remove(course);
        
        await _context.SaveChangesAsync(ct);
        
        return Result<int>.Success(course.Id);
        
    }
    
    
}