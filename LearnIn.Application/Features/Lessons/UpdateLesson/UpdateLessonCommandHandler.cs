using LearnIn.Application.Interfaces.Persistence;
using LearnIn.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnIn.Application.Features.Lessons.UpdateLesson;

public sealed class UpdateLessonCommandHandler : IRequestHandler<UpdateLessonCommand, Result<int>>
{
    private readonly IDataContext _context;
    
    public UpdateLessonCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(UpdateLessonCommand command, CancellationToken ct)
    {
        var lesson = await _context.Lessons.
            Include(l => l.Course).
            FirstOrDefaultAsync(l => l.Id == command.LessonId, ct);
        
        if (lesson == null)
        {
            return Result<int>.Failure(Errors.CourseNotFound());
        }

        if (lesson.Course.TeacherId != command.TeacherId)
        {
            return Result<int>.Failure(Errors.Forbidden());
        }

        lesson.Title = command.Title;
        lesson.Description =  command.Description;
        lesson.VideoUrl = command.VideoUrl;
        
        await _context.SaveChangesAsync(ct);
        return Result<int>.Success(lesson.Id);
        
    }
}