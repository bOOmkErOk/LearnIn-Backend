using MediatR;
using Microsoft.EntityFrameworkCore;
using LearnIn.Application.Interfaces.Persistence;
using LearnIn.Domain.Entities.Courses;
using LearnIn.Domain.Results;


namespace LearnIn.Application.Features.Lessons;

public class AddLessonCommandHandler : IRequestHandler<AddLessonCommand, Result<int>>
{
    private readonly IDataContext _context;

    public AddLessonCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(AddLessonCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            return Result<int>.Failure(Errors.ValidationFailed());
        }

        if (string.IsNullOrWhiteSpace(request.VideoUrl))
        {
            return Result<int>.Failure(Errors.VideoUrlEmpty());
        }

        if (request.SequenceNumber <= 0)
        {
            return Result<int>.Failure(Errors.SequenceNumberInvalid());
        }

        var course = await _context.Courses
            .FirstOrDefaultAsync(c => c.Id == request.CourseId, cancellationToken);

        if (course == null)
        {
            return Result<int>.Failure(Errors.CourseNotFound());
        }

        if (course.TeacherId != request.TeacherId)
        {
            return Result<int>.Failure(Errors.Forbidden());
        }

        var lesson = new Lesson
        {
            CourseId = request.CourseId,
            Title = request.Title,
            Description = request.Description,
            VideoUrl = request.VideoUrl,
            DurationSeconds = request.DurationSeconds,
            SequenceNumber = request.SequenceNumber
        };

        await _context.Lessons.AddAsync(lesson, cancellationToken);
        var result = await _context.SaveChangesAsync(cancellationToken);

        return result > 0 
            ? Result<int>.Success(lesson.Id) 
            : Result<int>.Failure(Errors.ServerError());
    }
}