using LearnIn.Application.Interfaces.Persistence;
using LearnIn.Domain.Enums.Users;
using LearnIn.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnIn.Application.Features.Lessons.DeleteLesson;

public sealed class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonCommand, Result<int>>
{
    private readonly IDataContext _context;
    
    public DeleteLessonCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(DeleteLessonCommand command, CancellationToken ct)
    {
        var lesson = await _context.Lessons
            .Include(l => l.Course)
            .FirstOrDefaultAsync(l => l.Id == command.LessonId, ct);

        if (lesson == null)
        {
            return Result<int>.Failure(Errors.LessonNotFound());
        }

        if (lesson.Course.TeacherId != command.TeacherId)
        {
            return Result<int>.Failure(Errors.Forbidden());
        }
        
        var teacher = await _context.Users.FirstOrDefaultAsync(u => u.Role == UserEnums.Teacher && u.Id == command.TeacherId, ct);

        if (teacher == null)
        {
            return Result<int>.Failure(Errors.TeacherNotFound());
        }
        _context.Lessons.Remove(lesson);
        var result = await _context.SaveChangesAsync(ct);
        return Result<int>.Success(result);
        
        
    }
}